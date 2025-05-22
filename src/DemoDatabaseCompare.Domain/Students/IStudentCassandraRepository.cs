using System.Collections.Generic;
using System.Threading.Tasks;
using DemoCompare.Cassandra.Entities;

namespace DemoCompare.Cassandra.Repositories
{
    public interface IStudentCassandraRepository
    {
        ValueTask InsertManyAsync(List<StudentCassandraEntity> students);
        ValueTask<List<StudentCassandraEntity>> GetPagedAsync(int page, int pageSize);
    }
} 