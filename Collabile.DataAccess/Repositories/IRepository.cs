using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabile.DataAccess.Repositories
{
    public interface IRepository<TEntity,Summary> where TEntity : class 
    {
        Task<List<Summary>> FetchAll(string filter);
        Task<TEntity> FetchById(string Id);
        Task<string> Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(string Id);
    }
}
