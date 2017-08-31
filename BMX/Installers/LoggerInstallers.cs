namespace Bmx.Abp.Installers
{
    using Bmx.Abp.Logging;

    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    public class LoggerInstallers : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For(typeof(ILogger)).ImplementedBy(typeof(Logging.Logger)));
        }
    }
}
