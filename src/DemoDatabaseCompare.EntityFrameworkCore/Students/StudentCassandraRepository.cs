using System;
using AutoMapper;
using Cassandra;
using Cassandra.Mapping;
using DemoCompare.Cassandra.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CassandraMapper = Cassandra.Mapping.IMapper;
using CassandraMapperImpl = Cassandra.Mapping.Mapper;

namespace DemoCompare.Cassandra.Repositories
{
    public class StudentCassandraRepository : IStudentCassandraRepository
    {
        private readonly ISession _session;
        private readonly CassandraMapper _mapper;

        public StudentCassandraRepository(ISession session)
        {
            _session = session;
            _mapper = new CassandraMapperImpl(_session);
        }

            public async Task InsertManyAsync(List<StudentCassandraEntity> students)
            {
                await Parallel.ForEachAsync(students, new ParallelOptions
                {
                    MaxDegreeOfParallelism = Environment.ProcessorCount * 2
                }, async (student, ct) =>
                {
                    await _mapper.InsertAsync(student);
                });
            }

            public async Task<List<StudentCassandraEntity>> GetPagedAsync(int page, int pageSize)
            {
                var cql = "SELECT * FROM students";

                byte[] pagingState = null;
                RowSet rs = null;

                for (int i = 0; i <= page; i++)
                {
                    var statement = new SimpleStatement(cql).SetPageSize(pageSize);

                    if (pagingState != null)
                    {
                        statement.SetPagingState(pagingState);
                    }

                    rs = await _session.ExecuteAsync(statement);
                    pagingState = rs.PagingState;
                }

                var result = rs.Select(row => new StudentCassandraEntity
                {
                    StudentId = row.GetValue<string>("studentid"),
                    FirstName = row.GetValue<string>("firstname"),
                    LastName = row.GetValue<string>("lastname"),
                    DateOfBirth = new DateTimeOffset(row.GetValue<DateTime>("dateofbirth")),
                    Grade = row.GetValue<string>("grade"),
                    Address = row.GetValue<string>("address")
                }).ToList();

                return result;
            }

    }
} 