﻿namespace Bmx.Abp.EntityFramework
{
    using Castle.MicroKernel.Registration;

    using Infrastructure;

    public class EfConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            context.Container.Register(Classes.FromAssembly(context.Assembly)
                .BasedOn<BaseDbContext>().WithService.Base()
                .Configure(
                    c =>
                        {
                            c.LifestyleTransient();
                        }));
        }
    }
}
