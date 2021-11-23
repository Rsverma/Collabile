using Dapper;
using System.Collections.Generic;
using System.Data;

namespace Collabile.Api.DataAccess
{
    public interface ISqlDataAccess
    {
        void CommitTransaction(IDbConnection connection, IDbTransaction transaction);

        List<T> LoadData<T, U>(string storedProcedure, U parameters);

        Task<T> LoadSingleAsync<T, U>(string storedProcedure, U parameters);

        SqlMapper.GridReader LoadMultiple<U>(string storedProcedure, U parameters);

        List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters, IDbConnection connection, IDbTransaction transaction);

        void RollbackTransaction(IDbConnection connection, IDbTransaction transaction);

        Task<int> SaveDataAsync<T>(string storedProcedure, T parameters);

        int SaveDataInTransaction<T>(string storedProcedure, T parameters, IDbConnection connection, IDbTransaction transaction);

        int SaveDataScalar<T>(string storedProcedure, T parameters);

        int SaveDataScalarInTransaction<T>(string storedProcedure, T parameters, IDbConnection connection, IDbTransaction transaction);

        void StartTransaction(out IDbConnection connection, out IDbTransaction transaction);
    }
}