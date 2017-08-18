namespace EFCodeFirstToAnExistingDatabase
{
    using System;

    using BMX;
    using BMX.Domain.Repositories;
    using BMX.Installers;

    class Program
    {
        static void Main(string[] args)
        {
            ConfigManager.EntityFrameworkConfiguration.ConnectionString =
                "Data Source=.;Initial Catalog=WIMIBTL6;Connect Timeout=30;persist security info=True;user id=sa;password=P@ssw0rd;MultipleActiveResultSets=True;";

            var container = new BMXBootstrapper().GetContainer();

            container.Install(new DbContextsInstaller(), new RepositoriesInstaller());

            IRepository<PartsOnlineRecord, int> partsOnlineRecordRepository = container.Resolve<IRepository<PartsOnlineRecord, int>>();

            var totalCount = partsOnlineRecordRepository.Count();

            IRepository<PartsOnlineRecord> partsOnlineRecordRepository2 = container.Resolve<IRepository<PartsOnlineRecord>>();

            var totalCount2 = partsOnlineRecordRepository2.Count();

            Console.ReadKey();
        }
    }
}
