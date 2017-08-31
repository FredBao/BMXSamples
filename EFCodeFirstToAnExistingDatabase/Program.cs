namespace EFCodeFirstToAnExistingDatabase
{
    using System;
    using System.Reflection;

    using Bmx.Abp.Configuration;
    using Bmx.Abp.EntityFramework;
    using Bmx.Abp.Infrastructure;
    using Bmx.Abp.MongoDb;

    using Castle.MicroKernel.Registration;

    public static class Program
    {
        public static void Main(string[] args)
        {
            ConfigManager.EntityFrameworkConfiguration.ConnectionString =
                "Data Source=.;Initial Catalog=WIMIBTL6;Connect Timeout=30;persist security info=True;user id=sa;password=P@ssw0rd;MultipleActiveResultSets=True;";

            ConfigManager.LogConfiguration.DebugMode = true;

            ConfigManager.MongoDbConfiguration.ConnectionString = "mongodb://btlsystem:123qwe@192.168.0.108:27017/WIMIBTL";

            ConfigManager.MongoDbConfiguration.DatabaseName = "WIMIBTL";

            var bootstrapper = new Bootstrapper(IocManager.Instance);

            bootstrapper.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly(), new EfConventionalRegistrar());

            bootstrapper.IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly(), new MongoDbConventionalRegistrar());

            bootstrapper.Initialize();

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
