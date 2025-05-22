using System.Collections.Generic;
using System.Threading.Tasks;
using DemoDatabaseCompare.Application.Contracts.Students;

namespace DemoDatabaseCompare.Students
{
    public interface IStudentScyllaService
    {
        ValueTask<List<StudentScyllaDto>> GetPagedAsync(int page, int pageSize);
        ValueTask InsertManyAsync(List<StudentScyllaDto> inputs);
    }
} 