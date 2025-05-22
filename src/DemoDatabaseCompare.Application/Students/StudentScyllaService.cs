using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoDatabaseCompare.Application.Contracts.Students;
using Volo.Abp.Application.Services;
using Microsoft.Extensions.Configuration;
using Cassandra;
using System.Linq;

namespace DemoDatabaseCompare.Students
{
    public class StudentScyllaService : ApplicationService, IStudentScyllaService
    {
        private readonly ISession _session;

        public StudentScyllaService(IConfiguration config)
        {
            var cluster = Cluster.Builder()
                .AddContactPoint(config["ScyllaDB:Host"])
                .Build();
            _session = cluster.Connect(config["ScyllaDB:Keyspace"]);
        }

        public async ValueTask<List<StudentScyllaDto>> GetPagedAsync(int page, int pageSize)
        {
            var query = "SELECT * FROM students LIMIT ?";
            var rs = await _session.ExecuteAsync(new SimpleStatement(query, page * pageSize));
            // Paging thủ công (ScyllaDB không hỗ trợ OFFSET)
            var all = rs.Select(row => new StudentScyllaDto
            {
                StudentId = row.GetValue<Guid>("studentid"),
                FirstName = row.GetValue<string>("firstname"),
                LastName = row.GetValue<string>("lastname"),
                DateOfBirth = row.GetValue<System.DateTime>("dateofbirth"),
                Grade = row.GetValue<string>("grade"),
                Address = row.GetValue<string>("address")
            }).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return all;
        }

        public async ValueTask InsertManyAsync(List<StudentScyllaDto> inputs)
        {
            var batch = new BatchStatement();
            foreach (var student in inputs)
            {
                var query = "INSERT INTO students (studentid, firstname, lastname, dateofbirth, grade, address) VALUES (?, ?, ?, ?, ?, ?)";
                batch.Add(new SimpleStatement(query,
                    student.StudentId, student.FirstName, student.LastName,
                    student.DateOfBirth, student.Grade, student.Address));
            }
            await _session.ExecuteAsync(batch);
        }
    }
} 