using DemoDatabaseCompare.Students;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Bogus;

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
        public async Task<IActionResult> Generate(int count)
        {
            var faker = new Faker<StudentDto>()
                .RuleFor(s => s.StudentId, f => f.Random.AlphaNumeric(8).ToUpper()) // 👈 THÊM DÒNG NÀY
                .RuleFor(s => s.FirstName, f => f.Name.FirstName())
                .RuleFor(s => s.LastName, f => f.Name.LastName())
                .RuleFor(s => s.DateOfBirth, f => f.Date.Past(20, DateTime.Now.AddYears(-18)))
                .RuleFor(s => s.Grade, f => f.Random.String2(2, "ABCDEF"))
                .RuleFor(s => s.Address, f => f.Address.FullAddress());


            var students = faker.Generate(count);
            var stopwatch = Stopwatch.StartNew();
            await _studentAppService.InsertManyAsync(students);
            stopwatch.Stop();
            _logger.LogInformation($"Inserted {count} students in {stopwatch.ElapsedMilliseconds} ms");
            return Ok(new { Count = count, ElapsedMilliseconds = stopwatch.ElapsedMilliseconds });
        }

        [HttpGet("read/{count}")]
        public async Task<IActionResult> Read(int count)
        {
            var stopwatch = Stopwatch.StartNew();
            var students = await _studentAppService.GetAllAsync(count);
            stopwatch.Stop();
            _logger.LogInformation($"Read {students.Count} students in {stopwatch.ElapsedMilliseconds} ms");
            return Ok(new { Count = students.Count, ElapsedMilliseconds = stopwatch.ElapsedMilliseconds, Data = students });
        }

        [HttpGet("read-paged")]
        public async Task<IActionResult> ReadPaged(int page = 1, int pageSize = 10)
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