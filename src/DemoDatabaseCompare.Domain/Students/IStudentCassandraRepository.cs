using System.Collections.Generic;
using System.Threading.Tasks;
using DemoCompare.Cassandra.Entities;

namespace DemoCompare.Cassandra.Repositories
{
    public interface IStudentCassandraRepository
    {
        Task InsertManyAsync(List<StudentCassandraEntity> students);
        Task<List<StudentCassandraEntity>> GetPagedAsync(int page, int pageSize);
    }
} 