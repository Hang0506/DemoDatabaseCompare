using Cassandra;
using DemoCompare.Cassandra.Entities;
using System;
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

        private static IEnumerable<List<T>> ChunkBy<T>(List<T> source, int chunkSize)
        {
            for (int i = 0; i < source.Count; i += chunkSize)
                yield return source.GetRange(i, Math.Min(chunkSize, source.Count - i));
        }

        public async ValueTask InsertManyAsync(List<StudentCassandraEntity> students)
        {
            const int batchSize = 100;

            // Prepare statement 1 lần
            var statement = _session.Prepare(
                "INSERT INTO students (studentid, firstname, lastname, dateofbirth, grade, address) " +
                "VALUES (?, ?, ?, ?, ?, ?)"
            );

            foreach (var batch in ChunkBy(students, batchSize))
            {
                var batchStatement = new BatchStatement();

                foreach (var student in batch)
                {
                    var bound = statement.Bind(
                        student.StudentId,        // UUID
                        student.FirstName,
                        student.LastName,
                        student.DateOfBirth,
                        student.Grade,
                        student.Address
                    );

                    batchStatement.Add(bound);
                }

                await _session.ExecuteAsync(batchStatement).ConfigureAwait(false);
            }
        }


        public async ValueTask<List<StudentCassandraEntity>> GetPagedAsync(int page, int pageSize)
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
                StudentId = row.GetValue<Guid>("studentid"),
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