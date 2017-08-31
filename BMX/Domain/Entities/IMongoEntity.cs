namespace Bmx.Abp.Domain.Entities
{
    using MongoDB.Bson;
    using MongoDB.Driver;

    public interface IMongoEntity : IEntity<ObjectId>
    {
        string CollectionName { get; }

        IMongoCollection<TEntity> GetCollection<TEntity>(IMongoDatabase mongoDatabase) where TEntity : class;
    }
}
