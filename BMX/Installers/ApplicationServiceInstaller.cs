namespace Bmx.Abp.Installers
{
    using Bmx.Abp.Infrastructure;
    using Bmx.Abp.Logging;

    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    public class ApplicationServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyInThisApplication().BasedOn<IApplicationService>().WithServiceAllInterfaces().LifestyleTransient());
        }
    }
}
