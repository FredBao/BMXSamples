namespace Bmx.Abp.MongoDb
{
    using MongoDB.Driver;

    public interface IMongoDbContext
    {
        IMongoDatabase MongoDatabase { get; set; }
    }
}
