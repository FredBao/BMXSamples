namespace EFCodeFirstToAnExistingDatabase
{
    using System.Data.Entity;

    public class StandDataContext : DbContext
    {
        public StandDataContext() : base("Data Source=.;Initial Catalog=WIMIBTL6;Connect Timeout=30;persist security info=True;user id=sa;password=P@ssw0rd;MultipleActiveResultSets=True;")
        {
        }

        public virtual DbSet<PartsOnlineRecord> PartsOnlineRecords { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
