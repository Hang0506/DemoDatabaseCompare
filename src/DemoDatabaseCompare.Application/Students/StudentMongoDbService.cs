using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace DemoDatabaseCompare.Students
{
    public class StudentMongoDbService : ApplicationService, IStudentMongoDbService
    {
        private readonly IMongoCollection<StudentMongoDto> _collection;

        public StudentMongoDbService(IConfiguration config)
        {
            var settings = MongoClientSettings.FromConnectionString(config["MongoDB:ConnectionString"]);
            var client = new MongoClient(settings);
            var db = client.GetDatabase(config["MongoDB:Database"]);
            _collection = db.GetCollection<StudentMongoDto>("Students");
        }

        public async ValueTask<List<StudentMongoDto>> GetPagedAsync(int page, int pageSize)
        {
            return await _collection.Find(_ => true)
                .Skip((page - 1) * pageSize)
                .Limit(pageSize)
                .ToListAsync();
        }

        public async ValueTask InsertManyAsync(List<StudentMongoDto> inputs)
        {
            await _collection.InsertManyAsync(inputs);
        }
    }
} 