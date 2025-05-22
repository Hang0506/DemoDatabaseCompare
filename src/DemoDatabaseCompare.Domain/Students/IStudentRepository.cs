using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace DemoDatabaseCompare.Students
{
    public interface IStudentRepository : IRepository<Student, Guid>
    {
        Task InsertManyAsync(List<Student> students);
        ValueTask<int> GetTotalCountAsync();
        Task<List<Student>> GetPagedListAsync(int page, int pageSize);
    }
} 