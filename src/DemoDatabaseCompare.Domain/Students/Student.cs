using System;
using Volo.Abp.Domain.Entities;

namespace DemoDatabaseCompare.Students
{
    public class Student : Entity<Guid>
    {
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Grade { get; set; }
        public string Address { get; set; }

        public Student() { }
        public Student(Guid id, string studentId, string firstName, string lastName, DateTime dateOfBirth, string grade, string address)
            : base(id)
        {
            StudentId = studentId;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Grade = grade;
            Address = address;
        }
    }
} 