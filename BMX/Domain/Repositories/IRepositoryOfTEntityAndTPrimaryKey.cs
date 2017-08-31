namespace Bmx.Abp.Domain.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using Entities;

    /// <summary>
    /// This interface is implemented by all repositories to ensure implementation of fixed methods.
    /// </summary>
    /// <typeparam name="TEntity">Main Entity type this repository works on</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public interface IRepository<TEntity, TPrimaryKey> : IRepository where TEntity : class, IEntity<TPrimaryKey>
    {
        long Count();

        long Count(Expression<Func<TEntity, bool>> predicate);

        TEntity Get(TPrimaryKey id);

        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> GetAllList();

        IEnumerable<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);

        void Insert(TEntity entity);

        void InsertMany(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(TPrimaryKey id);

        void DeleteMany(IEnumerable<TEntity> entities);
    }
}
