using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Collabile.Api.DataAccess
{
    public class SqlDataAccess : IDisposable, ISqlDataAccess
    {
        public SqlDataAccess(IConfiguration config, ILogger<SqlDataAccess> logger)
        {
            _config = config;
            _logger = logger;
        }

        internal string GetConnectionString(string name)
        {
            return _config.GetConnectionString(name);
        }

        public List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using IDbConnection connection = new SqlConnection(connectionString);
            List<T> rows = connection.Query<T>(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure).ToList();
            return rows;
        }

        public int SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            using IDbConnection connection = new SqlConnection(connectionString);
            return connection.Execute(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure);
        }

        public int SaveDataScalar<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);


            using IDbConnection connection = new SqlConnection(connectionString);
            return connection.ExecuteScalar<int>(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure);
        }

        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private readonly IConfiguration _config;
        private readonly ILogger<SqlDataAccess> _logger;

        public void StartTransaction(string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);

            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters)
        {
            List<T> rows = _connection.Query<T>(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure, transaction: _transaction).ToList();
            return rows;
        }

        public int SaveDataInTransaction<T>(string storedProcedure, T parameters)
        {
            return _connection.Execute(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure, transaction: _transaction);
        }

        public int SaveDataScalarInTransaction<T>(string storedProcedure, T parameters)
        {
            return _connection.ExecuteScalar<int>(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure, transaction: _transaction);
        }

        public void CommitTransaction()
        {
            _transaction?.Commit();
            _transaction = null;
            _connection?.Close();
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _transaction = null;
            _connection?.Close();
        }

        public void Dispose()
        {
            try
            {
                _transaction = null;
                _connection?.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sql data access dispose failed");
            }
        }
    }
}