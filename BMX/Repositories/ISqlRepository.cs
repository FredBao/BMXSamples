namespace BMX.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using BMX;

    /// <summary>
    /// 适用于使用写sql语句的ORM
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface ISqlRepository<TEntity, TPrimaryKey> : IRepository, ITransientDependency where TEntity : class, IEntity<TPrimaryKey>
    {
        long Count();

        long Count(dynamic predicate);

        TEntity Get(TPrimaryKey id);

        TEntity Get(dynamic predicate);

        IEnumerable<TEntity> GetAllList();

        void Insert(TEntity entity);

        TPrimaryKey InsertAndGetId(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(TPrimaryKey id);
    }
}