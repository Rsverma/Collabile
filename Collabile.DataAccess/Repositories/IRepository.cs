using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabile.DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        List<TEntity> FetchAll();
        TEntity FetchById(string Id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(string Id);
    }
}
