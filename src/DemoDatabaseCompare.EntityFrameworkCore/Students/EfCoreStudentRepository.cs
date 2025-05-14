using System;
using DemoDatabaseCompare.EntityFrameworkCore;
using DemoDatabaseCompare.Students;
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
    }
} 