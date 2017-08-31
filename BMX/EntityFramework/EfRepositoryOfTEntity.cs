namespace Bmx.Abp.EntityFramework
{
    using System.Data.Entity;

    using Domain.Entities;
    using Domain.Repositories;

    public class EfRepository<TEntity> : EfRepository<TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        public EfRepository(BaseContext context) : base(context)
        {
        }
    }
}
