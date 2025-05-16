using DemoDatabaseCompare.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace DemoDatabaseCompare.Students
{
    public class EfCoreStudentRepository : EfCoreRepository<DemoDatabaseCompareDbContext, Student, Guid>, IStudentRepository
    {
        public EfCoreStudentRepository(IDbContextProvider<DemoDatabaseCompareDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task InsertManyAsync(List<Student> students)
        {
            var dbContext = await GetDbContextAsync();
            await dbContext.Students.AddRangeAsync(students);
            await dbContext.SaveChangesAsync();
        }
    }
} 