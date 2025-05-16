using Cassandra.Data.Linq;
using System;

namespace DemoDatabaseCompare.Application.Contracts.Students
{
    public class StudentCassandraDto
    {
        [PartitionKey]
        [Cassandra.Data.Linq.Column("studentid")]
        public string StudentId { get; set; } = string.Empty;

        [Cassandra.Data.Linq.Column("firstname")]
        public string FirstName { get; set; } = string.Empty;

        [Cassandra.Data.Linq.Column("lastname")]
        public string LastName { get; set; } = string.Empty;

        [Cassandra.Data.Linq.Column("dateofbirth")]
        public DateTime DateOfBirth { get; set; }

        [Cassandra.Data.Linq.Column("grade")]
        public string Grade { get; set; } = string.Empty;

        [Cassandra.Data.Linq.Column("address")]
        public string Address { get; set; } = string.Empty;
    }
} 