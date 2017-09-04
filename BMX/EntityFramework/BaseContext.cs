namespace Bmx.Abp.EntityFramework
{
    using System.Data.Entity;

    public class BaseDbContext : DbContext
    {
        public BaseDbContext(string connectionStringOrName) : base(connectionStringOrName)
        {
        }
    }
}
