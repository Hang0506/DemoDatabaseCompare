using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoCompare.Cassandra.Entities;
using DemoDatabaseCompare.Application.Contracts.Students;
using DemoCompare.Cassandra.Repositories;
using DemoDatabaseCompare.Students;
using Volo.Abp.Application.Services;

namespace DemoCompare.Cassandra.Services
{
    public class StudentCassandraService : ApplicationService, IStudentCassandraService
    {
        private readonly IStudentCassandraRepository _repository;

        public StudentCassandraService(IStudentCassandraRepository repository)
        {
            _repository = repository;
        }
        
        public async ValueTask InsertManyAsync(List<StudentCassandraDto> students)
        {
            var studentEntities = students.Select(student => new StudentCassandraEntity(student.StudentId, student.FirstName, student.LastName, student.DateOfBirth, student.Grade, student.Address)).ToList();
            await _repository.InsertManyAsync(studentEntities);
        }

        public async ValueTask<List<StudentCassandraDto>> GetPagedAsync(int page, int pageSize)
        {
            var studentEntities = await _repository.GetPagedAsync(page, pageSize);
            var studentDtos = studentEntities.Select(entity => new StudentCassandraDto {
                StudentId = entity.StudentId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                DateOfBirth = entity.DateOfBirth.UtcDateTime,
                Grade = entity.Grade,
                Address = entity.Address
            }).ToList();
            return studentDtos;
        }

        public async ValueTask ClearAllAsync()
        {
            await _repository.ClearAllAsync();
        }
    }
} 