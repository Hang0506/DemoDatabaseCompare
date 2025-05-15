using System;
using DemoDatabaseCompare.EntityFrameworkCore;
using DemoDatabaseCompare.Students;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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