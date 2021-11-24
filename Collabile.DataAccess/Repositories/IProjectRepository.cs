using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabile.DataAccess.Repositories
{
    public interface IProjectRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
    }
}
