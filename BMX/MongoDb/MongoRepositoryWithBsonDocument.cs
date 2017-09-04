namespace Bmx.Abp.MongoDb
{
    using System;
    using System.Collections.Generic;

    using Domain.Entities;

    using MongoDB.Bson;
    using MongoDB.Driver;

    public class MongoRepository<TEntity, TDocument> : IMongoRepository<TEntity, TDocument>
        where TEntity : class
        where TDocument : BsonDocument
    {
        private readonly IMongoCollection<TDocument> mongoConnCollection;

        public MongoRepository(IMongoDbContext mongoDbContext)
        {
            this.mongoConnCollection = this.GetCollection(mongoDbContext);
        }

        public long Count()
        {
            return this.mongoConnCollection.Count(Builders<TDocument>.Filter.Empty);
        }

        public long Count(FilterDefinition<TDocument> filter)
        {
            return this.mongoConnCollection.Count(filter);
        }

        public void DeleteOne(FilterDefinition<TDocument> filter)
        {
            this.mongoConnCollection.DeleteOne(filter);
        }

        public void DeleteMany(FilterDefinition<TDocument> filter)
        {
            this.mongoConnCollection.DeleteMany(filter);
        }

        public TDocument Get(FilterDefinition<TDocument> filter)
        {
            return this.mongoConnCollection.Find(filter).FirstOrDefault();
        }

        public IEnumerable<TDocument> GetMany(FilterDefinition<TDocument> filter)
        {
            return this.mongoConnCollection.Find(filter).ToList();
        }

        public IEnumerable<TDocument> GetAll()
        {
            return this.mongoConnCollection.Find(Builders<TDocument>.Filter.Empty).ToList();
        }

        public void InsertOne(TDocument entity)
        {
            this.mongoConnCollection.InsertOne(entity);
        }

        public void InsertMany(IEnumerable<TDocument> entities)
        {
            this.mongoConnCollection.InsertMany(entities);
        }

        public void UpdateOne(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update)
        {
            this.mongoConnCollection.UpdateOne(filter, update);
        }

        public void UpdateMany(FilterDefinition<TDocument> filter, UpdateDefinition<TDocument> update)
        {
            this.mongoConnCollection.UpdateMany(filter, update);
        }

        private IMongoCollection<TDocument> GetCollection(IMongoDbContext mongoDbContext)
        {
            var entityType = typeof(TEntity);
            var entityInstance = Activator.CreateInstance(entityType);
            var collection = ((IMongoEntity)entityInstance).GetCollection<TDocument>(mongoDbContext.MongoDatabase);
            return collection;
        }
    }
}
