namespace EFCodeFirstToAnExistingDatabase
{
    using Bmx.Abp.MongoDb;

    public class StandDataMongoContext : MongoDbContext
    {
        public StandDataMongoContext() : base("mongodb://btlsystem:123qwe@192.168.0.108:27017/WIMIBTL", "WIMIBTL")
        {
        }
    }
}
