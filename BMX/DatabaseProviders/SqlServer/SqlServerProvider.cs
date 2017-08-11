namespace BMX.DatabaseProviders.SqlServer
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Transactions;

    using Dapper;

    public class SqlServerProvider : ISqlServerProvider
    {
        private readonly string connectionString;

        public SqlServerProvider()
        {
            this.connectionString = ConfigManager.SqlServerConnection;
        }

        public long ExcuteCountQuery(string command)
        {
            long result;

            using (var conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                result = conn.ExecuteScalar<long>(command);
            }

            return result;
        }

        public long ExcuteCountQuery(string command, CommittableTransaction committran)
        {
            long result;

            using (var conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                conn.EnlistTransaction(committran);

                result = conn.ExecuteScalar<long>(command);
            }

            return result;
        }

        public long ExcuteCountQuery(string command, object parameters)
        {
            long result;

            using (var conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                result = conn.ExecuteScalar<long>(command, parameters);
            }

            return result;
        }

        public long ExcuteCountQuery(string command, object parameters, CommittableTransaction committran)
        {
            long result;

            using (var conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                conn.EnlistTransaction(committran);

                result = conn.ExecuteScalar<long>(command, parameters);
            }

            return result;
        }

        public int ExcuteNonQuery(string command, object parameters)
        {
            int result;

            using (var conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                result = conn.Execute(command, parameters);
            }

            return result;
        }

        public int ExcuteNonQuery(string command, object parameters, CommittableTransaction committran)
        {
            int result;

            using (var conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                conn.EnlistTransaction(committran);

                result = conn.Execute(command, parameters);
            }

            return result;
        }

        public IEnumerable<TEntity> ExcuteQuery<TEntity>(string command) where TEntity : class
        {
            IEnumerable<TEntity> result;

            using (var conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                result = conn.Query<TEntity>(command).ToList();
            }

            return result;
        }

        public IEnumerable<TEntity> ExcuteQuery<TEntity>(string command, CommittableTransaction committran) where TEntity : class
        {
            IEnumerable<TEntity> result;

            using (var conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                conn.EnlistTransaction(committran);

                result = conn.Query<TEntity>(command).ToList();
            }

            return result;
        }

        public IEnumerable<TEntity> ExcuteQuery<TEntity>(string command, object parameters)
            where TEntity : class
        {
            IEnumerable<TEntity> result;

            using (var conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                result = conn.Query<TEntity>(command, parameters).ToList();
            }

            return result;
        }

        public IEnumerable<TEntity> ExcuteQuery<TEntity>(string command, object parameters, CommittableTransaction committran)
            where TEntity : class
        {
            IEnumerable<TEntity> result;

            using (var conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                conn.EnlistTransaction(committran);

                result = conn.Query<TEntity>(command, parameters).ToList();
            }

            return result;
        }
    }
}