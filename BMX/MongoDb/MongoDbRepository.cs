namespace Bmx.Abp.MongoDb
{
    using System;
    using System.Collections.Generic;

    using Domain.Entities;

    using MongoDB.Driver;

    public class MongoDbRepository<TEntity> : IMongoDbRepository<TEntity> where TEntity : class
    {
        private readonly IMongoCollection<TEntity> mongoDbConnCollection;

        public MongoDbRepository(IMongoDatabase mongoDatabase)
        {
            this.mongoDbConnCollection = this.GetCollection(mongoDatabase);
        }

        public long Count()
        {
            return this.mongoDbConnCollection.Count(Builders<TEntity>.Filter.Empty);
        }

        public long Count(FilterDefinition<TEntity> filter)
        {
            return this.mongoDbConnCollection.Count(filter);
        }

        public void DeleteOne(FilterDefinition<TEntity> filter)
        {
            this.mongoDbConnCollection.DeleteOne(filter);
        }

        public void DeleteMany(FilterDefinition<TEntity> filter)
        {
            this.mongoDbConnCollection.DeleteMany(filter);
        }

        public TEntity Get(FilterDefinition<TEntity> filter)
        {
            return this.mongoDbConnCollection.Find(filter).FirstOrDefault();
        }

        public IEnumerable<TEntity> GetMany(FilterDefinition<TEntity> filter)
        {
            return this.mongoDbConnCollection.Find(filter).ToList();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.mongoDbConnCollection.Find(Builders<TEntity>.Filter.Empty).ToList();
        }

        public void InsertOne(TEntity entity)
        {
            this.mongoDbConnCollection.InsertOne(entity);
        }

        public void InsertMany(IEnumerable<TEntity> entities)
        {
            this.mongoDbConnCollection.InsertMany(entities);
        }

        public void UpdateOne(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update)
        {
            this.mongoDbConnCollection.UpdateOne(filter, update);
        }

        public void UpdateMany(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update)
        {
            this.mongoDbConnCollection.UpdateMany(filter, update);
        }

        public IMongoCollection<TEntity> GetCollection(IMongoDatabase mongoDatabase)
        {
            var entityType = typeof(TEntity);
            var entityInstance = Activator.CreateInstance(entityType);
            var collection = ((IMongoEntity)entityInstance).GetCollection<TEntity>(mongoDatabase);
            return collection;
        }
    }
}
