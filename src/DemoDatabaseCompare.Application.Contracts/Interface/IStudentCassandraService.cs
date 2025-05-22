using System.Collections.Generic;
using System.Threading.Tasks;
using DemoDatabaseCompare.Application.Contracts.Students;

namespace DemoDatabaseCompare.Students
{
    public interface IStudentCassandraService
    {
        ValueTask<List<StudentCassandraDto>> GetPagedAsync(int page, int pageSize);
        ValueTask InsertManyAsync(List<StudentCassandraDto> inputs);
    }
} 