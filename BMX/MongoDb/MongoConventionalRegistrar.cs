namespace Bmx.Abp.MongoDb
{
    using Castle.MicroKernel.Registration;

    using Infrastructure;

    public class MongoConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            context.Container.Register(Classes.FromAssembly(context.Assembly)
                .BasedOn<IMongoDbContext>().WithService.Base()
                .Configure(
                    c =>
                        {
                            c.LifestyleTransient();
                        }));
        }
    }
}
