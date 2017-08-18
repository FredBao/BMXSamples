namespace BMX.Installers
{
    using System;
    using System.Data.Entity;
    using System.Reflection;

    using BMX.EntityFramework;

    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Castle.Windsor.Installer;

    public class DbContextsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //container.Register(Classes.FromThisAssembly()
            //    .BasedOn<BaseContext>()
            //    .WithService.DefaultInterfaces()
            //    .Configure(
            //    c =>
            //    {
            //        c.LifestyleSingleton();
            //        c.DependsOn(Dependency.OnValue("connectionStringOrName", ConfigManager.EntityFrameworkConfiguration.ConnectionString));
            //    }));
        }
    }
}
