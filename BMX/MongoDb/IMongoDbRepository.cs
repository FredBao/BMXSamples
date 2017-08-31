namespace Bmx.Abp.MongoDb
{
    using System.Collections.Generic;

    using Domain.Repositories;

    using MongoDB.Driver;

    public interface IMongoDbRepository<TEntity> : IRepository 
        where TEntity : class
    {
        long Count();

        long Count(FilterDefinition<TEntity> filter);

        void DeleteOne(FilterDefinition<TEntity> filter);

        void DeleteMany(FilterDefinition<TEntity> filter);

        TEntity Get(FilterDefinition<TEntity> filter);

        IEnumerable<TEntity> GetMany(FilterDefinition<TEntity> filter);

        IEnumerable<TEntity> GetAll();

        void InsertOne(TEntity entity);

        void InsertMany(IEnumerable<TEntity> entities);

        void UpdateOne(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update);

        void UpdateMany(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update); 

        IMongoCollection<TEntity> GetCollection(IMongoDatabase mongoDatabase);
    }
}
