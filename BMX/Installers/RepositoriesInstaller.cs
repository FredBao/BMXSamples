namespace Bmx.Abp.Installers
{
    using Bmx.Abp.MongoDb;

    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using Domain.Repositories; 

    using EntityFramework;

    using MongoDB.Bson;

    public class RepositoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For(typeof(IRepository<,>)).ImplementedBy(typeof(EfRepository<,>)).LifestyleTransient());

            container.Register(Component.For(typeof(IRepository<>)).ImplementedBy(typeof(EfRepository<>)).LifestyleTransient());

            container.Register(Component.For(typeof(IMongoRepository<>)).ImplementedBy(typeof(MongoRepository<>)));

            container.Register(Component.For(typeof(IMongoRepository<,>)).ImplementedBy(typeof(MongoRepository<,>)));
        }
    }
}
