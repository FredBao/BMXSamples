namespace BMX
{
    using BMX.Installers;

    using Castle.Windsor;

    public class BMXBootstrapper
    {
        public IWindsorContainer GetContainer()
        {
            var container = new WindsorContainer();

            return container;
        }
    }
}
