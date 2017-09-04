namespace Bmx.Abp.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Linq.Expressions;

    using Bmx.Abp.Domain.Entities;
    using Bmx.Abp.Domain.Repositories;

    public class EfRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly BaseDbContext context;

        public EfRepository(BaseDbContext context)
        {
            this.context = context;
        }

        private DbSet<TEntity> Table => this.context.Set<TEntity>();

        public long Count()
        {
            return this.Table.Count();
        }

        public long Count(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Table.Count(predicate);
        }

        public void Delete(TPrimaryKey id)
        {
            var entity = this.Get(id);

            this.Delete(entity);
        }

        public void Delete(TEntity entity)
        {
            this.Table.Remove(entity);
        }

        public void DeleteMany(IEnumerable<TEntity> entities)
        {
            this.Table.RemoveRange(entities);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Table.FirstOrDefault(predicate);
        }

        public TEntity Get(TPrimaryKey id)
        {
            return this.Table.Find(id);
        }

        public IEnumerable<TEntity> GetAllList()
        {
            return this.Table.ToList();
        }

        public IEnumerable<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Table.Where(predicate).ToList();
        }

        public void Insert(TEntity entity)
        {
            this.Table.Add(entity);
        }

        public void InsertMany(IEnumerable<TEntity> entities)
        {
            this.Table.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            this.Table.AddOrUpdate(entity);
        }
    }
}
