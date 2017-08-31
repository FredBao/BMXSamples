namespace EFCodeFirstToAnExistingDatabase
{
    using System;

    using Bmx.Abp.Domain.Entities;

    using MongoDB.Bson;
    using MongoDB.Driver;

    public class Parameter : MongoDbEntity
    {
        public string Code { get; set; }

        public string CreationTime { get; set; }

        public string MachineCode { get; set; }

        public int MachineId { get; set; }

        public int MachinesShiftDetailId { get; set; }

        public string Message { get; set; }

        public int OrderId { get; set; }

        public string PartNo { get; set; }

        public int ProcessId { get; set; }

        public string ProgramName { get; set; }

        public long UserId { get; set; }

        public int UserShiftDetailId { get; set; }

        public override string CollectionName
        {
            get
            {
                return "Parameter" + DateTime.Now.ToString("yyyyMM");
            }
        }

        public override IMongoCollection<TEntity> GetCollection<TEntity>(IMongoDatabase mongoDatabase)
        {
            var filter = new BsonDocument("name", this.CollectionName);

            var collections = mongoDatabase.ListCollections(new ListCollectionsOptions { Filter = filter });

            var count = collections.ToList().Count;

            if (count == 0)
            {
                mongoDatabase.CreateCollection(this.CollectionName);
            }

            var collection = mongoDatabase.GetCollection<TEntity>(this.CollectionName);

            // 为每个月创建的参数集合创建索引,提升查询性能
            if (!collection.Indexes.List().Any())
            {
                collection.Indexes.CreateOne(
                    new BsonDocument("MachineId", 1).Add("PartNo", 1).Add("StepNo", 1).Add("CreationTime", -1));
            }

            return collection;
        }
    }
}
