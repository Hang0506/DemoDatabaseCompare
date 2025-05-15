using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DemoDatabaseCompare.Students
{
    public class StudentAppService : ApplicationService , IStudentAppService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentAppService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<StudentDto> InsertAsync(StudentDto input)
        {
            var student = new Student(Guid.NewGuid(),input.StudentId, input.FirstName, input.LastName, input.DateOfBirth, input.Grade, input.Address);
            student = await _studentRepository.InsertAsync(student, autoSave: true);
            return ObjectMapper.Map<Student, StudentDto>(student);
        }

        public async Task<List<StudentDto>> GetAllAsync(int count)
        {
            var students = await _studentRepository.GetListAsync();
            return students.Take(count).Select(x => ObjectMapper.Map<Student, StudentDto>(x)).ToList();
        }

        public async Task<List<StudentDto>> GetPagedAsync(int page, int pageSize)
        {
            var students = await _studentRepository.GetListAsync();
            return students.Skip((page - 1) * pageSize).Take(pageSize).Select(x => ObjectMapper.Map<Student, StudentDto>(x)).ToList();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return (int)await _studentRepository.GetCountAsync();
        }

        public async Task InsertManyAsync(List<StudentDto> inputs)
        {
            var students = inputs.Select(input => new Student(Guid.NewGuid(), input.StudentId, input.FirstName, input.LastName, input.DateOfBirth, input.Grade, input.Address)).ToList();
            await _studentRepository.InsertManyAsync(students);
        }
        
    }
} 