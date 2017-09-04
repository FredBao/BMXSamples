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

        void RegisterAssemblyByConvention(Assembly assembly, bool installInstallers = false, params IConventionalDependencyRegistrar[] registerers);

        bool IsRegistered(Type type);

        bool IsRegistered<T>();
    }
}
