using System.Collections.Generic;
using System.Threading.Tasks;
using DemoDatabaseCompare.Application.Contracts.Students;

namespace DemoDatabaseCompare.Students
{
    public interface IStudentCassandraService
    {
        Task<List<StudentCassandraDto>> GetPagedAsync(int page, int pageSize);
        Task InsertManyAsync(List<StudentCassandraDto> inputs);
    }
} 