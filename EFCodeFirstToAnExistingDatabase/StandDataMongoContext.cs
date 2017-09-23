namespace EFCodeFirstToAnExistingDatabase
{
    using Bmx.Abp.MongoDb;

    public class StandDataMongoContext : MongoDbContext
    {
        public StandDataMongoContext() : base("mongodb://btlsystem:123qwe@127.0.0.1:27017/WIMIBTL", "WIMIBTL")
        {
        }
    }
}
