using Collabile.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collabile.DataAccess.Repositories
{
    public class ProjectRepository<TEntity> : IProjectRepository<TEntity> where TEntity : Project
    {
        public void Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string Id)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> FetchAll()
        {
            throw new NotImplementedException();
        }

        public TEntity FetchById(string Id)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
