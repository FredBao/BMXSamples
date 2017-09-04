namespace Bmx.Abp.Domain.Entities
{
    using System;

    using MongoDB.Bson;
    using MongoDB.Driver;

    [Serializable]
    public abstract class MongoEntity : IMongoEntity
    {
        public ObjectId Id { get; set; }

        public virtual string CollectionName { get; }

        public virtual IMongoCollection<TEntity> GetCollection<TEntity>(IMongoDatabase mongoDatabase) where TEntity : class
        {
            var filter = new BsonDocument("name", this.CollectionName);

            var collections = mongoDatabase.ListCollections(new ListCollectionsOptions { Filter = filter });

            var count = collections.ToList().Count;

            if (count == 0)
            {
                mongoDatabase.CreateCollection(this.CollectionName);
            }

            var collection = mongoDatabase.GetCollection<TEntity>(this.CollectionName);

            return collection;
        }
    }
}
