namespace Bmx.Abp.Infrastructure
{
    using Bmx.Abp.EntityFramework;

    using Castle.MicroKernel.Registration;

    public class AppConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            context.Container.Register(
                Classes.FromAssembly(context.Assembly).BasedOn<IApplicationService>().WithServiceAllInterfaces()
                    .LifestyleTransient());
        }
    }
}
