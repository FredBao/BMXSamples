namespace BMX.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using BMX;

    public interface ILinqRepository<TEntity, TPrimaryKey> : IRepository, ITransientDependency where TEntity : class, IEntity<TPrimaryKey>
    {
        long Count();

        long Count(Expression<Func<TEntity, bool>> predicate);

        TEntity Get(TPrimaryKey id);

        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetAllList();

        IEnumerable<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Update(Expression<Func<TEntity, bool>> predicate, TEntity entity);

        void Delete(TEntity entity);

        void Delete(TPrimaryKey id);

        void Delete(Expression<Func<TEntity, bool>> predicate);
    }
}