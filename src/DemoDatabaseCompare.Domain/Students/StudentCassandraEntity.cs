using Cassandra.Mapping.Attributes;
using System;
using CassandraColumn = Cassandra.Mapping.Attributes.ColumnAttribute;

namespace DemoCompare.Cassandra.Entities
{
    [Table("students")]
    public class StudentCassandraEntity
    {
        [PartitionKey]
        [CassandraColumn("studentid")]
        public Guid StudentId { get; set; }

        [CassandraColumn("firstname")]
        public string FirstName { get; set; } = string.Empty;

        [CassandraColumn("lastname")]
        public string LastName { get; set; } = string.Empty;

        [CassandraColumn("dateofbirth")]
        public DateTimeOffset DateOfBirth { get; set; }

        [CassandraColumn("grade")]
        public string Grade { get; set; } = string.Empty;

        [CassandraColumn("address")]
        public string Address { get; set; } = string.Empty;

        public StudentCassandraEntity() { }

        public StudentCassandraEntity(Guid studentId, string firstName, string lastName, DateTimeOffset dateOfBirth, string grade, string address)
        {
            StudentId = studentId;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Grade = grade;
            Address = address;
        }

        public static StudentCassandraEntity FromDto(StudentCassandraEntity dto)
        {
            return new StudentCassandraEntity(
                dto.StudentId,
                dto.FirstName,
                dto.LastName,
                dto.DateOfBirth,
                dto.Grade,
                dto.Address
            );
        }

        public StudentCassandraEntity ToDto()
        {
            return new StudentCassandraEntity
            {
                StudentId = this.StudentId,
                FirstName = this.FirstName,
                LastName = this.LastName,
                DateOfBirth = this.DateOfBirth,
                Grade = this.Grade,
                Address = this.Address
            };
        }
    }
} 