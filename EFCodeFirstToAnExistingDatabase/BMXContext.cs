namespace EFCodeFirstToAnExistingDatabase
{
    using System.Data.Entity;

    public class BMXContext : DbContext
    {
        public BMXContext() : base("Data Source=.;Initial Catalog=WIMIBTL6;user id=sa;password=P@ssw0rd;MultipleActiveResultSets=True;Connect Timeout=120;persist security info=True;")
        {
        }

        public virtual DbSet<PartsOnlineRecord> PartsOnlineRecords { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
