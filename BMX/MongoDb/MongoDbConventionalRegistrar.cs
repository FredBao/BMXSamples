namespace Bmx.Abp.MongoDb
{
    using System;
    using System.Linq;

    using Castle.MicroKernel.Registration;

    using Configuration;

    using Domain.Entities;

    using Infrastructure;

    using MongoDB.Bson;
    using MongoDB.Driver;

    public class MongoDbConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            var mongoDbEntityTypes = context.Assembly.GetTypes().Where(s => s.BaseType == typeof(MongoDbEntity));

            var mongoDatabase = this.GetMongoDatabase();

            // 为每一个MongoDb中的类型创建集合
            foreach (var mongoDbEntityType in mongoDbEntityTypes)
            {
                var serviceType = typeof(IMongoDbRepository<>).MakeGenericType(mongoDbEntityType);

                var implementedType = typeof(MongoDbRepository<>).MakeGenericType(mongoDbEntityType);

                context.Container.Register(Component.For(serviceType)
                    .ImplementedBy(implementedType)
                    .DependsOn(Dependency.OnValue(typeof(IMongoDatabase), mongoDatabase)));
            }

            foreach (var mongoDbEntityType in mongoDbEntityTypes)
            {
                var serviceType = typeof(IMongoDbRepository<,>).MakeGenericType(mongoDbEntityType, typeof(BsonDocument));

                var implementedType = typeof(MongoDbRepository<,>).MakeGenericType(mongoDbEntityType, typeof(BsonDocument));

                context.Container.Register(Component.For(serviceType)
                    .ImplementedBy(implementedType)
                    .DependsOn(Dependency.OnValue(typeof(IMongoDatabase), mongoDatabase)));
            }
        }

        private IMongoDatabase GetMongoDatabase()
        {
            var client = new MongoClient(ConfigManager.MongoDbConfiguration.ConnectionString);
            var db = client.GetDatabase(ConfigManager.MongoDbConfiguration.DatabaseName);
            return db;
        }
    }
}
