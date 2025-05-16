using System;
using DemoDatabaseCompare.Application.Contracts.Students;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Bogus;
using DemoDatabaseCompare.Students;

namespace DemoCompare.Cassandra.Controllers
{
    [ApiController]
    [Route("api/cassandra/students")]
    public class StudentCassandraController : ControllerBase
    {
        private readonly IStudentCassandraService _studentService;

        public StudentCassandraController(IStudentCassandraService studentService)
        {
            _studentService = studentService;
        }

      
        [HttpGet("generate/{count}")]
        public async Task<IActionResult> Generate(int count)
        {
            var faker = new Faker<StudentCassandraDto>()
                .RuleFor(s => s.StudentId, f => f.Random.AlphaNumeric(8).ToUpper()) // 👈 THÊM DÒNG NÀY
                .RuleFor(s => s.FirstName, f => f.Name.FirstName())
                .RuleFor(s => s.LastName, f => f.Name.LastName())
                .RuleFor(s => s.DateOfBirth, f => f.Date.Past(20, DateTime.Now.AddYears(-18)))
                .RuleFor(s => s.Grade, f => f.Random.String2(2, "ABCDEF"))
                .RuleFor(s => s.Address, f => f.Address.FullAddress());


            var students = faker.Generate(count);
            var stopwatch = Stopwatch.StartNew();
            await _studentService.InsertManyAsync(students);
            stopwatch.Stop();
            return Ok(new { Count = count, ElapsedMilliseconds = stopwatch.ElapsedMilliseconds });
        }
        [HttpGet("paged")]
        public async Task<ActionResult<List<StudentCassandraDto>>> GetPagedAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _studentService.GetPagedAsync(page, pageSize);
            return Ok(result);
        }
    }
} 