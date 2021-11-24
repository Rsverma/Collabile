using Collabile.DataAccess.Models;

namespace Collabile.DataAccess.Repositories
{
    public interface IProjectRepository<TEntity> : IRepository<TEntity> where TEntity : Project
    {
    }
}
