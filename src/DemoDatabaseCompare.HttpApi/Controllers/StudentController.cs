using DemoDatabaseCompare.Students;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Bogus;
using System.Linq;
using System.Threading;

namespace DemoDatabaseCompare.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentAppService _studentAppService;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IStudentAppService studentAppService, ILogger<StudentController> logger)
        {
            _studentAppService = studentAppService;
            _logger = logger;
        }

        [HttpGet("generate/{count}")]
        public async ValueTask<IActionResult> Generate(int count)
        {
            var faker = new Faker<StudentDto>()
                .RuleFor(s => s.StudentId, f => f.Random.AlphaNumeric(8).ToUpper())
                .RuleFor(s => s.FirstName, f => f.Name.FirstName())
                .RuleFor(s => s.LastName, f => f.Name.LastName())
                .RuleFor(s => s.DateOfBirth, f => f.Date.Past(20, DateTime.Now.AddYears(-18)))
                .RuleFor(s => s.Grade, f => f.Random.String2(2, "ABCDEF"))
                .RuleFor(s => s.Address, f => f.Address.FullAddress());

            var stopwatch = Stopwatch.StartNew();

            const int batchSize = 10000;
            const int maxConcurrency = 4;
            var totalBatches = (int)Math.Ceiling(count / (double)batchSize);
            var semaphore = new SemaphoreSlim(maxConcurrency);

            var tasks = Enumerable.Range(0, totalBatches).Select(async batchIndex =>
            {
                await semaphore.WaitAsync();
                try
                {
                    var batchCount = Math.Min(batchSize, count - batchIndex * batchSize);
                    var students = faker.Generate(batchCount);
                    await _studentAppService.InsertManyAsync(students);
                }
                finally
                {
                    semaphore.Release();
                }
            });

            await Task.WhenAll(tasks);

            stopwatch.Stop();
            _logger.LogInformation($"Inserted {count} students in {stopwatch.ElapsedMilliseconds} ms");

            return Ok(new { Count = count, ElapsedMilliseconds = stopwatch.ElapsedMilliseconds });
        }

        [HttpGet("read/{count}")]
        public async ValueTask<IActionResult> Read(int count)
        {
            var stopwatch = Stopwatch.StartNew();
            var students = await _studentAppService.GetAllAsync(count);
            stopwatch.Stop();
            _logger.LogInformation($"Read {students.Count} students in {stopwatch.ElapsedMilliseconds} ms");
            return Ok(new { Count = students.Count, ElapsedMilliseconds = stopwatch.ElapsedMilliseconds, Data = students });
        }

        [HttpGet("read-paged")]
        public async ValueTask<IActionResult> ReadPaged(int page = 1, int pageSize = 10)
        {
            var stopwatch = Stopwatch.StartNew();
            // Lấy tổng số học sinh
            var total = await _studentAppService.GetTotalCountAsync();
            // Lấy danh sách học sinh theo trang
            var students = await _studentAppService.GetPagedAsync(page, pageSize);
            stopwatch.Stop();
            _logger.LogInformation($"Read page {page} ({students.Count} students) in {stopwatch.ElapsedMilliseconds} ms");
            return Ok(new
            {
                Count = students.Count,
                Total = total,
                Page = page,
                PageSize = pageSize,
                ElapsedMilliseconds = stopwatch.ElapsedMilliseconds,
                Data = students
            });
        }
    }
}