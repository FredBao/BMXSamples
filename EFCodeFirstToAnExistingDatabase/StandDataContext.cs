namespace EFCodeFirstToAnExistingDatabase
{
    using System.Data.Entity;

    using Bmx.Abp.EntityFramework;
    using Bmx.Abp.Logging;

    public class StandDataDbContext : BaseDbContext
    {
        public StandDataDbContext() : base("Default")
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
