namespace Bmx.Abp.Installers
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using Domain.Repositories; 

    using EntityFramework;

    public class RepositoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For(typeof(IRepository<,>)).ImplementedBy(typeof(EfRepository<,>)).LifestyleTransient());

            container.Register(Component.For(typeof(IRepository<>)).ImplementedBy(typeof(EfRepository<>)).LifestyleTransient());
        }
    }
}
