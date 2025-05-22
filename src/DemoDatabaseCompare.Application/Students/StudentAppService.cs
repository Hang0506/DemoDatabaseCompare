using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DemoDatabaseCompare.Students
{
    public class StudentAppService : ApplicationService, IStudentAppService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentAppService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async ValueTask<List<StudentDto>> GetAllAsync(int count)
        {
            var students = await _studentRepository.GetListAsync();
            return students.Take(count).Select(x => ObjectMapper.Map<Student, StudentDto>(x)).ToList();
        }

        public async ValueTask<List<StudentDto>> GetPagedAsync(int page, int pageSize)
        {
            var students = await _studentRepository.GetPagedListAsync(page, pageSize);
            return students.Select(x => new StudentDto
            {
                Id = x.Id,
                StudentId = x.StudentId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DateOfBirth = x.DateOfBirth,
                Grade = x.Grade,
                Address = x.Address
            }).ToList();
        }

        public async ValueTask<int> GetTotalCountAsync()
        {
            return await _studentRepository.GetTotalCountAsync();
        }

        public async ValueTask InsertManyAsync(List<StudentDto> inputs)
        {
            var students = inputs.Select(input =>
                new Student(Guid.NewGuid(), input.StudentId, input.FirstName, input.LastName, input.DateOfBirth, input.Grade, input.Address)
            ).ToList();

            await _studentRepository.InsertManyAsync(students);
        }
    }
}