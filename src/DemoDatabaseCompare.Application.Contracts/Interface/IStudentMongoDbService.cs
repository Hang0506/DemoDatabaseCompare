using System.Collections.Generic;
using System.Threading.Tasks;
using DemoDatabaseCompare.Application.Contracts.Students;

namespace DemoDatabaseCompare.Students
{
    public interface IStudentMongoDbService
    {
        ValueTask<List<StudentMongoDto>> GetPagedAsync(int page, int pageSize);
        ValueTask InsertManyAsync(List<StudentMongoDto> inputs);
        ValueTask ClearAllAsync();
    }
} 