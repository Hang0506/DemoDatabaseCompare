﻿using System;
using DemoDatabaseCompare.Students;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Bogus;
using DemoDatabaseCompare.Application.Contracts.Students;

namespace DemoDatabaseCompare.Scylla.Controllers
{
    [ApiController]
    [Route("api/scylla/students")]
    public class StudentScyllaController : ControllerBase
    {
        private readonly IStudentScyllaService _studentService;

        public StudentScyllaController(IStudentScyllaService studentService)
        {
            _studentService = studentService;
        }


        [HttpGet("generate/{count}")]
        public async ValueTask<IActionResult> Generate(int count)
        {
            var stopwatch = Stopwatch.StartNew();

            var students = GenerateFakeStudents(count);

            await _studentService.InsertManyAsync(students);

            stopwatch.Stop();

            return Ok(new
            {
                Count = count,
                ElapsedMilliseconds = stopwatch.ElapsedMilliseconds
            });
        }

        private List<StudentScyllaDto> GenerateFakeStudents(int count)
        {
            var faker = new Faker<StudentScyllaDto>()
                .RuleFor(s => s.StudentId, f => Guid.NewGuid())
                .RuleFor(s => s.FirstName, f => f.Name.FirstName())
                .RuleFor(s => s.LastName, f => f.Name.LastName())
                .RuleFor(s => s.DateOfBirth, f => f.Date.Past(20, DateTime.Now.AddYears(-18)))
                .RuleFor(s => s.Grade, f => f.Random.String2(2, "ABCDEF"))
                .RuleFor(s => s.Address, f => f.Address.FullAddress());

            return faker.Generate(count);
        }

        [HttpGet("paged")]
        public async ValueTask<ActionResult<List<StudentScyllaDto>>> GetPagedAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _studentService.GetPagedAsync(page, pageSize);
            return Ok(result);
        }

        [HttpDelete("clear")]
        public async ValueTask<IActionResult> Clear()
        {
            await _studentService.ClearAllAsync();
            return Ok(new { Success = true });
        }
    }
}