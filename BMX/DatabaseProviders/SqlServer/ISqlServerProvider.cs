namespace BMX.DatabaseProviders.SqlServer
{
    using System.Collections.Generic;
    using System.Transactions;

    public interface ISqlServerProvider : ITransientDependency
    {
        long ExcuteCountQuery(string command);

        long ExcuteCountQuery(string command, CommittableTransaction committran);

        long ExcuteCountQuery(string command, object parameters);

        long ExcuteCountQuery(string command, object parameters, CommittableTransaction committran);

        int ExcuteNonQuery(string command, object parameters);

        int ExcuteNonQuery(string command, object parameters, CommittableTransaction committran);

        IEnumerable<TEntity> ExcuteQuery<TEntity>(string command) where TEntity : class;

        IEnumerable<TEntity> ExcuteQuery<TEntity>(string command, CommittableTransaction committran) where TEntity : class;

        IEnumerable<TEntity> ExcuteQuery<TEntity>(string command, object parameters) where TEntity : class;

        IEnumerable<TEntity> ExcuteQuery<TEntity>(string command, object parameters, CommittableTransaction committran) where TEntity : class;
    }
}
