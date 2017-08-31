namespace Bmx.Abp.Infrastructure
{
    using System;

    using Castle.Windsor;
    using Castle.Windsor.Installer;

    public class Bootstrapper
    {
        public Bootstrapper(IIocManager iocManager)
        {
            this.IocManager = iocManager;
        }

        public IIocManager IocManager { get; }

        /// <summary>
        /// Initializes the ABP system.
        /// </summary>
        public virtual void Initialize()
        {
            try
            {
                this.IocManager.IocContainer.Install(FromAssembly.This());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
