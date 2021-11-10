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
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private readonly string connectionString;
        private readonly ILogger<SqlDataAccess> _logger;

        public SqlDataAccess(IConfiguration config, ILogger<SqlDataAccess> logger)
        {
            connectionString = config.GetConnectionString("CollabileData");
            _logger = logger;
        }

        public List<T> LoadData<T, U>(string storedProcedure, U parameters)
        {
            using IDbConnection connection = new SqlConnection(connectionString);
            List<T> rows = connection.Query<T>(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure).ToList();
            return rows;
        }

        public int SaveData<T>(string storedProcedure, T parameters)
        {
            using IDbConnection connection = new SqlConnection(connectionString);
            return connection.Execute(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure);
        }

        public int SaveDataScalar<T>(string storedProcedure, T parameters)
        {
            using IDbConnection connection = new SqlConnection(connectionString);
            return connection.ExecuteScalar<int>(storedProcedure, parameters,
                commandType: CommandType.StoredProcedure);
        }

        public void StartTransaction()
        {
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