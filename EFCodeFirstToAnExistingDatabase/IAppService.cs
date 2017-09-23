namespace EFCodeFirstToAnExistingDatabase
{
    using Bmx.Abp.Infrastructure;

    public interface IAppService : IApplicationService, ITransientDependency
    {
        long Count();

        long AlarmCount();

        void CreateAlarm();

        long ParameterCount();

        void CreateParameter();
    }
}
