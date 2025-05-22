using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DemoDatabaseCompare.Students
{
    public class StudentMongoDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; } = new Guid();
        public string StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Grade { get; set; }
        public string Address { get; set; }
    }
}