using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoDatabaseCompare.Students
{
    public interface IStudentAppService
    {
        Task<StudentDto> InsertAsync(StudentDto input);
        Task<List<StudentDto>> GetAllAsync(int count);
        Task<List<StudentDto>> GetPagedAsync(int page, int pageSize);
        Task<int> GetTotalCountAsync();
        Task InsertManyAsync(List<StudentDto> inputs);
    }
}