using DemoDatabaseCompare.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DemoDatabaseCompare.Students
{
    public class StudentAppService : ApplicationService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentAppService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<StudentDto> InsertAsync(StudentDto input)
        {
            var student = new Student(Guid.NewGuid(), input.StudentId, input.FirstName, input.LastName, input.DateOfBirth, input.Grade, input.Address);
            student = await _studentRepository.InsertAsync(student, autoSave: true);
            return ObjectMapper.Map<Student, StudentDto>(student);
        }

        public async Task<List<StudentDto>> GetAllAsync(int count)
        {
            var students = await _studentRepository.GetListAsync();
            return students.Take(count).Select(x => ObjectMapper.Map<Student, StudentDto>(x)).ToList();
        }
    }
} 