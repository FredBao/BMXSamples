namespace BMX.DatabaseProviders.Mongodb
{
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class MongodbProvider : IMongodbProvider
    {
        private const string FilterName = "name";

        public IMongoDatabase MongoDatabase
        {
            get
            {
                var client = new MongoClient(ConfigManager.MongoConnectionString);
                var db = client.GetDatabase(ConfigManager.MongoDatabaseName);
                return db;
            }
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>(string collectionName) where TEntity : class
        {
            return this.MongoDatabase.GetCollection<TEntity>(collectionName);
        }

        public bool CollectionExist(string collectionName)
        {
            var filter = new BsonDocument(FilterName, collectionName);

            var collections = this.MongoDatabase.ListCollections(new ListCollectionsOptions { Filter = filter });

            var count = collections.ToList().Count;

            return count > 0;
        }

        public void CreateCollection(string collectionName)
        {
            this.MongoDatabase.CreateCollection(collectionName);
        }

        public void CreateCollectionWithIndex<TEntity>(string collectionName, BsonDocument indexBsonDocument) where TEntity : class
        {
            this.MongoDatabase.CreateCollection(collectionName);

            var collection = this.MongoDatabase.GetCollection<TEntity>(collectionName);

            collection.Indexes.CreateOne(indexBsonDocument);
        }
    }
}