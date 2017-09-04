namespace Bmx.Abp.MongoDb
{
    using System;
    using System.Collections.Generic;

    using Domain.Entities;

    using MongoDB.Driver;

    public class MongoRepository<TEntity> : IMongoRepository<TEntity> where TEntity : class
    {
        private readonly IMongoCollection<TEntity> mongoConnCollection;

        public MongoRepository(IMongoDbContext mongoDbContext)
        {
            this.mongoConnCollection = this.GetCollection(mongoDbContext);
        }

        public long Count()
        {
            return this.mongoConnCollection.Count(Builders<TEntity>.Filter.Empty);
        }

        public long Count(FilterDefinition<TEntity> filter)
        {
            return this.mongoConnCollection.Count(filter);
        }

        public void DeleteOne(FilterDefinition<TEntity> filter)
        {
            this.mongoConnCollection.DeleteOne(filter);
        }

        public void DeleteMany(FilterDefinition<TEntity> filter)
        {
            this.mongoConnCollection.DeleteMany(filter);
        }

        public TEntity Get(FilterDefinition<TEntity> filter)
        {
            return this.mongoConnCollection.Find(filter).FirstOrDefault();
        }

        public IEnumerable<TEntity> GetMany(FilterDefinition<TEntity> filter)
        {
            return this.mongoConnCollection.Find(filter).ToList();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.mongoConnCollection.Find(Builders<TEntity>.Filter.Empty).ToList();
        }

        public void InsertOne(TEntity entity)
        {
            this.mongoConnCollection.InsertOne(entity);
        }

        public void InsertMany(IEnumerable<TEntity> entities)
        {
            this.mongoConnCollection.InsertMany(entities);
        }

        public void UpdateOne(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update)
        {
            this.mongoConnCollection.UpdateOne(filter, update);
        }

        public void UpdateMany(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update)
        {
            this.mongoConnCollection.UpdateMany(filter, update);
        }

        public IMongoCollection<TEntity> GetCollection(IMongoDbContext mongoDbContext)
        {
            var entityType = typeof(TEntity);
            var entityInstance = Activator.CreateInstance(entityType);
            var collection = ((IMongoEntity)entityInstance).GetCollection<TEntity>(mongoDbContext.MongoDatabase);
            return collection;
        }
    }
}
