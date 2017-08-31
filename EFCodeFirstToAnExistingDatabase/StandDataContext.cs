namespace EFCodeFirstToAnExistingDatabase
{
    using System.Data.Entity;

    using Bmx.Abp.EntityFramework;
    using Bmx.Abp.Logging;

    public class StandDataContext : BaseContext
    {
        public StandDataContext() : base("Data Source=.;Initial Catalog=WIMIBTL6;Connect Timeout=30;persist security info=True;user id=sa;password=P@ssw0rd;MultipleActiveResultSets=True;")
        {
        }

        public ILogger Logger { get; set; }

        public virtual DbSet<PartsOnlineRecord> PartsOnlineRecords { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.Logger.Info("OnModelCreating() Done.");
        }
    }
}
