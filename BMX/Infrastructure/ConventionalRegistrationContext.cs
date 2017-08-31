namespace Bmx.Abp.Infrastructure
{
    using System.Reflection;
    using Castle.Windsor;

    public class ConventionalRegistrationContext : IConventionalRegistrationContext
    {
        internal ConventionalRegistrationContext(Assembly assembly, IWindsorContainer container)
        {
            this.Assembly = assembly;
            this.Container = container;
        }

        public Assembly Assembly { get; private set; }

        public IWindsorContainer Container { get; private set; }
    }
}
