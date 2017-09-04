namespace EFCodeFirstToAnExistingDatabase
{
    using System.Collections.Generic;

    using Bmx.Abp.Domain.Repositories;
    using Bmx.Abp.MongoDb;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class AppService
    {
        private readonly IRepository<PartsOnlineRecord> repository;

        private readonly IMongoRepository<Alarm> alarmRepository;

        private readonly IMongoRepository<Parameter, BsonDocument> parameterRepository;

        public AppService(
            IRepository<PartsOnlineRecord> repository,
            IMongoRepository<Alarm> alarmRepository,
            IMongoRepository<Parameter, BsonDocument> parameterRepository)
        {
            this.repository = repository;
            this.alarmRepository = alarmRepository;
            this.parameterRepository = parameterRepository;
        }

        public Bmx.Abp.Logging.ILogger Logger { get; set; }

        public long Count()
        {
            return this.repository.Count();
        }

        public long AlarmCount()
        {
            return this.alarmRepository.Count();
        }

        public void CreateAlarm()
        {
            var entity = new Alarm()
            {
                Code = "ssssssss"
            };
            this.alarmRepository.InsertOne(entity);
        }

        public long ParameterCount()
        {
            return this.parameterRepository.Count();
        }
        public void CreateParameter()
        {
            IDictionary<string, object> parameterDocument = new Dictionary<string, object>();

            parameterDocument.Add("PartNo", "1111111");

            parameterDocument.Add("MachineId", "1111111");

            parameterDocument.Add("CreationTime", "1111111");

            this.parameterRepository.InsertOne(parameterDocument.ToBsonDocument());
        }
    }
}
