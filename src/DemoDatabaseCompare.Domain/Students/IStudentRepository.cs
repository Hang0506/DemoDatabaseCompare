using System;
using Volo.Abp.Domain.Repositories;

namespace DemoDatabaseCompare.Students
{
    public interface IStudentRepository : IRepository<Student, Guid>
    {
    }
} 