namespace Bmx.Abp.MongoDb
{
    using System;
    using System.Collections.Generic;

    using Domain.Entities;

    using MongoDB.Bson;
    using MongoDB.Driver;

    public class MongoDbRepository<TEntity, TDocument> : IMongoDbRepository<TEntity, TDocument>
        where TEntity : class
        where TDocument : BsonDocument
    {
        private readonly IMongoCollection<TDocument> mongoDbConnCollection;

        public MongoDbRepository(IMongoDatabase mongoDatabase)
        {
            this.mongoDbConnCollection = this.GetCollection(mongoDatabase);
        }

        public long Count()
        {
            return this.mongoDbConnCollection.Count(Builders<TDocument>.Filter.Empty);
        }

        public long Count(FilterDefinition<TDocument> filter)
        {
            return this.mongoDbConnCollection.Count(filter);
        }

        public void DeleteOne(FilterDefinition<TDocument> filter)
        {
            this.mongoDbConnCollection.DeleteOne(filter);
        }

        public void DeleteMany(FilterDefinition<TDocument> filter)
        {
            this.mongoDbConnCollection.DeleteMany(filter);
        }

        public TDocument Get(FilterDefinition<TDocument> filter)
        {
            return this.mongoDbConnCollection.Find(filter).FirstOrDefault();
        }

        public IEnumerable<TDocument> GetMany(FilterDefinition<TDocument> filter)
        {
            return this.mongoDbConnCollection.Find(filter).ToList();
        }

        public IEnumerable<TDocument> GetAll()
        {
            return this.mongoDbConnCollection.Find(Builders<TDocument>.Filter.Empty).ToList();
        }

        public void InsertOne(TDocument entity)
        {
            this.mongoDbConnCollection.InsertOne(entity);
        }

        public void InsertMany(IEnumerable<TDocument> entities)
        {
            this.mongoDbConnCollection.InsertMany(entities);
        }

        public void UpdateOne(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update)
        {
            this.mongoDbConnCollection.UpdateOne(filter, update);
        }

        public void UpdateMany(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update)
        {
            this.mongoDbConnCollection.UpdateMany(filter, update);
        }

        public IMongoCollection<TDocument> GetCollection(IMongoDatabase mongoDatabase)
        {
            var entityType = typeof(TEntity);
            var entityInstance = Activator.CreateInstance(entityType);
            var collection = ((IMongoEntity)entityInstance).GetCollection<TDocument>(mongoDatabase);
            return collection;
        }
    }
}
