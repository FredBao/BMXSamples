namespace BMX.DatabaseProviders.Mongodb
{
    using MongoDB.Bson;
    using MongoDB.Driver;

    public interface IMongodbProvider : ITransientDependency
    {
        IMongoDatabase MongoDatabase { get; }

        IMongoCollection<TEntity> GetCollection<TEntity>(string collectionName) where TEntity : class;

        bool CollectionExist(string collectionName);

        void CreateCollection(string collectionName);

        void CreateCollectionWithIndex<TEntity>(string collectionName, BsonDocument indexBsonDocument) where TEntity : class;
    }
}
