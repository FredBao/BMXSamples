namespace Bmx.Abp.EntityFramework
{
    using System.Data.Entity;

    public class BaseContext : DbContext
    {
        public BaseContext(string connectionStringOrName) : base(connectionStringOrName)
        {
        }
    }
}
