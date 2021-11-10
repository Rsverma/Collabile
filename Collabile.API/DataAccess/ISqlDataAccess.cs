using System.Collections.Generic;

namespace Collabile.Api.DataAccess
{
    public interface ISqlDataAccess
    {
        void CommitTransaction();

        List<T> LoadData<T, U>(string storedProcedure, U parameters);

        List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters);

        void RollbackTransaction();

        int SaveData<T>(string storedProcedure, T parameters);

        int SaveDataInTransaction<T>(string storedProcedure, T parameters);

        int SaveDataScalar<T>(string storedProcedure, T parameters);

        int SaveDataScalarInTransaction<T>(string storedProcedure, T parameters);

        void StartTransaction();
    }
}