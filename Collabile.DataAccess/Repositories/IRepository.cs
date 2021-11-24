using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabile.DataAccess.Repositories
{
    public interface IRepository<TEntity,Summary> where TEntity : class 
    {
        List<Summary> FetchAll(string filter);
        TEntity FetchById(string Id);
        string Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(string Id);
    }
}
