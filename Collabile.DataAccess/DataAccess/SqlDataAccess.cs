using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Collabile.Api.DataAccess;
public class SqlDataAccess : ISqlDataAccess
{
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

    public async Task<T> LoadSingleAsync<T, U>(string storedProcedure, U parameters)
    {
        using IDbConnection connection = new SqlConnection(connectionString);
        T item = await connection.QuerySingleOrDefaultAsync<T>(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);
        return item;
    }

    public SqlMapper.GridReader LoadMultiple<U>(string storedProcedure, U parameters)
    {
        using IDbConnection connection = new SqlConnection(connectionString);
        SqlMapper.GridReader reader = connection.QueryMultiple(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);
        return reader;
    }

    public async Task<int> SaveDataAsync<T>(string storedProcedure, T parameters)
    {
        using IDbConnection connection = new SqlConnection(connectionString);
        return await connection.ExecuteAsync(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);
    }

    public int SaveDataScalar<T>(string storedProcedure, T parameters)
    {
        using IDbConnection connection = new SqlConnection(connectionString);
        return connection.ExecuteScalar<int>(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);
    }

    public void StartTransaction(out IDbConnection connection, out IDbTransaction transaction)
    {
        connection = new SqlConnection(connectionString);
        connection.Open();
        transaction = connection.BeginTransaction();
    }

    public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters, IDbConnection connection, IDbTransaction transaction)
    {
        List<T> rows = connection.Query<T>(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure, transaction: transaction).ToList();
        return rows;
    }

    public int SaveDataInTransaction<T>(string storedProcedure, T parameters, IDbConnection connection, IDbTransaction transaction)
    {
        return connection.Execute(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure, transaction: transaction);
    }

    public int SaveDataScalarInTransaction<T>(string storedProcedure, T parameters, IDbConnection connection, IDbTransaction transaction)
    {
        return connection.ExecuteScalar<int>(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure, transaction: transaction);
    }

    public void CommitTransaction(IDbConnection connection, IDbTransaction transaction)
    {
        transaction?.Commit();
        connection?.Close();
    }

    public void RollbackTransaction(IDbConnection connection, IDbTransaction transaction)
    {
        transaction?.Rollback();
        connection?.Close();
    }

}