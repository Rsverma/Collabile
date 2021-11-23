using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabile.DataAccess.Repositories
{
    internal interface IRepository<TEntity> where TEntity : class
    {
        List<TEntity> FetchAll();
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Save();
    }
}
