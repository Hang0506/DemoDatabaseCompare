using System;

namespace DemoDatabaseCompare.Students
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Grade { get; set; }
        public string Address { get; set; }
    }
} 