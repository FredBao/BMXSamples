namespace EFCodeFirstToAnExistingDatabase
{
    using System;
    using System.Reflection;

    using Bmx.Abp.EntityFramework;
    using Bmx.Abp.Infrastructure;
    using Bmx.Abp.MongoDb;

    using Castle.MicroKernel.Registration;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var bootstrapper = new Bootstrapper(IocManager.Instance);

            bootstrapper.Initialize();

            bootstrapper.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly(), false, new EfConventionalRegistrar(), new MongoConventionalRegistrar());
            
            bootstrapper.IocManager.IocContainer.Register(Component.For(typeof(AppService)).Named("AppService2"));

            var appService = bootstrapper.IocManager.IocContainer.Resolve<AppService>("AppService2");

            Console.WriteLine(appService.Count());

            Console.WriteLine(appService.AlarmCount());

            appService.CreateAlarm();

            Console.WriteLine(appService.ParameterCount());

            appService.CreateParameter();

            Console.WriteLine(appService.ParameterCount());

            Console.ReadKey();
        }
    }
}
