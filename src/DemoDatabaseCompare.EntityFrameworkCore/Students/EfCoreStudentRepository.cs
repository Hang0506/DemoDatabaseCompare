using DemoDatabaseCompare.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Uow;
using Polly;

namespace DemoDatabaseCompare.Students
{
    public class EfCoreStudentRepository : EfCoreRepository<DemoDatabaseCompareDbContext, Student, Guid>, IStudentRepository
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public EfCoreStudentRepository(
            IDbContextProvider<DemoDatabaseCompareDbContext> dbContextProvider, IUnitOfWorkManager unitOfWork )
            : base(dbContextProvider)
        {
            _unitOfWorkManager = unitOfWork;
        }

        public async Task InsertManyAsync(List<Student> students)
        {
            const int batchSize = 1000;
            int total = students.Count;

            for (int i = 0; i < total; i += batchSize)
            {
                var batch = students.GetRange(i, Math.Min(batchSize, total - i));

                using (var uow = _unitOfWorkManager.Begin(requiresNew: true)) // 👈 Mỗi batch là 1 UnitOfWork riêng
                {
                    var dbContext = await GetDbContextAsync(); // 👈 không tự dispose

                    await dbContext.Students.AddRangeAsync(batch);
                    await dbContext.SaveChangesAsync();

                    dbContext.ChangeTracker.Clear();

                    await uow.CompleteAsync(); // 👈 đảm bảo commit
                }
            }
        }
        public async ValueTask<int> GetTotalCountAsync()
        {
            var dbContext = await GetDbContextAsync();

            var count = dbContext.Students.AsNoTracking().Count();


            return count;
        }
        public async Task<List<Student>> GetPagedListAsync(int page, int pageSize)
        {
            var dbContext = await GetDbContextAsync();

            return await dbContext.Students.AsNoTracking()
                .OrderBy(s => s.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task ClearAllAsync()
        {
            var dbContext = await GetDbContextAsync();
            await dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE  dbo.AppStudents;");
        }
    }
}