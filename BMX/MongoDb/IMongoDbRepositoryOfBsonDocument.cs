namespace Bmx.Abp.MongoDb
{
    using System.Collections.Generic;

    using Domain.Repositories;

    using MongoDB.Bson;
    using MongoDB.Driver;

    public interface IMongoDbRepository<TEntity, TDocument> : IRepository
        where TEntity : class
        where TDocument : BsonDocument
    {
        long Count();

        long Count(FilterDefinition<TDocument> filter);

        void DeleteOne(FilterDefinition<TDocument> filter);

        void DeleteMany(FilterDefinition<TDocument> filter);

        TDocument Get(FilterDefinition<TDocument> filter);

        IEnumerable<TDocument> GetMany(FilterDefinition<TDocument> filter);

        IEnumerable<TDocument> GetAll();

        void InsertOne(TDocument entity);

        void InsertMany(IEnumerable<TDocument> entities);

        void UpdateOne(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update);

        void UpdateMany(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update);

        IMongoCollection<TDocument> GetCollection(IMongoDatabase mongoDatabase);
    }
}
