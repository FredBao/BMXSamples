namespace Bmx.Abp.MongoDb
{
    using MongoDB.Driver;

    public class MongoDbContext : IMongoDbContext
    {
        public MongoDbContext(string connectionString, string databaseName)
        {
            this.MongoDatabase = this.GetMongoDatabase(connectionString, databaseName);
        }

        public IMongoDatabase MongoDatabase { get; set; }

        private IMongoDatabase GetMongoDatabase(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase(databaseName);
            return db;
        }
    }
}
