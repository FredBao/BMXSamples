namespace Bmx.Abp.Infrastructure
{
    using System;
    using System.Reflection;

    using Castle.Windsor;
    using Castle.Windsor.Installer;

    public class IocManager : IIocManager
    {
        static IocManager()
        {
            Instance = new IocManager();
        }

        public IocManager()
        {
            this.IocContainer = new WindsorContainer();
        }
        
        public static IocManager Instance { get; private set; }
        
        public IWindsorContainer IocContainer { get; private set; }
        
        public void RegisterAssemblyByConvention(Assembly assembly, IConventionalDependencyRegistrar registerer, bool installInstallers = false)
        {
            var context = new ConventionalRegistrationContext(assembly, this.IocContainer);

            registerer.RegisterAssembly(context);

            if (installInstallers)
            {
                this.IocContainer.Install(FromAssembly.Instance(assembly));
            }
        }
        
        public bool IsRegistered(Type type)
        {
            return this.IocContainer.Kernel.HasComponent(type);
        }

        public bool IsRegistered<TType>()
        {
            return this.IocContainer.Kernel.HasComponent(typeof(TType));
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~IocManager() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
