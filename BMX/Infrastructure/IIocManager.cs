namespace Bmx.Abp.Infrastructure
{
    using System;
    using System.Reflection;

    using Castle.Windsor;

    /// <summary>
    /// This interface is used to directly perform dependency injection tasks.
    /// </summary>
    public interface IIocManager : IDisposable
    {
        IWindsorContainer IocContainer { get; }

        void RegisterAssemblyByConvention(Assembly assembly, IConventionalDependencyRegistrar registerer, bool installInstallers = false);

        bool IsRegistered(Type type);

        bool IsRegistered<T>();
    }
}
