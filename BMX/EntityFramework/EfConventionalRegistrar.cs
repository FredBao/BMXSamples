namespace Bmx.Abp.EntityFramework
{
    using Castle.MicroKernel.Registration;

    using Configuration;

    using Infrastructure;

    public class EfConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            context.Container.Register(Classes.FromAssembly(context.Assembly)
                .BasedOn<BaseContext>().WithService.Base()
                .Configure(
                    c =>
                        {
                            c.LifestyleTransient();
                            c.DependsOn(Dependency.OnValue("connectionStringOrName", ConfigManager.EntityFrameworkConfiguration.ConnectionString));
                        }));
        }
    }
}
