using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoDatabaseCompare.Application.Contracts.Students;
using Volo.Abp.Application.Services;
using Microsoft.Extensions.Configuration;
using Cassandra;
using System.Linq;
using System.Threading;

namespace DemoDatabaseCompare.Students
{
    public class StudentScyllaService : ApplicationService, IStudentScyllaService
    {
        private readonly ISession _session;
        private PreparedStatement _preparedInsert;
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

        private async Task EnsurePreparedAsync()
        {
            if (_preparedInsert == null)
            {
                _preparedInsert = await _session.PrepareAsync(
                    "INSERT INTO students (studentid, firstname, lastname, dateofbirth, grade, address) VALUES (?, ?, ?, ?, ?, ?)");
            }
        }

        public async ValueTask InsertManyAsync(List<StudentScyllaDto> students)
        {
            const int batchSize = 50; // thử nghiệm với 50, có thể tăng/giảm tùy cluster
            const int maxConcurrency = 10; // số lượng batch chạy song song, thử nghiệm với 10

            await EnsurePreparedAsync();

            using var throttler = new SemaphoreSlim(maxConcurrency);

            var tasks = new List<Task>();

            foreach (var chunk in students.Chunk(batchSize))
            {
                await throttler.WaitAsync();

                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        var writeTasks = chunk.Select(student =>
                        {
                            var bound = _preparedInsert.Bind(
                                student.StudentId,
                                student.FirstName,
                                student.LastName,
                                student.DateOfBirth,
                                student.Grade,
                                student.Address);
                            return _session.ExecuteAsync(bound);
                        });

                        await Task.WhenAll(writeTasks);
                    }
                    finally
                    {
                        throttler.Release();
                    }
                }));
            }

            await Task.WhenAll(tasks);
        }

        public async ValueTask ClearAllAsync()
        {
            var cql = "TRUNCATE students";
            await _session.ExecuteAsync(new SimpleStatement(cql));
        }
    }
} 