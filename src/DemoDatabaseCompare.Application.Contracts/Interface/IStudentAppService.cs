using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoDatabaseCompare.Students
{
    public interface IStudentAppService
    {
        ValueTask<List<StudentDto>> GetAllAsync(int count);
        ValueTask<List<StudentDto>> GetPagedAsync(int page, int pageSize);
        ValueTask<int> GetTotalCountAsync();
        ValueTask InsertManyAsync(List<StudentDto> inputs);
    }
}