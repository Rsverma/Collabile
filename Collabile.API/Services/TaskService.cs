using Collabile.DataAccess.Models;
using Collabile.Shared.Models;
using Collabile.Shared.Models.Items;
using System.Collections.Generic;

namespace Collabile.Api.Services
{
    public class TaskService : ITaskService
    {
        public void DeleteTask(int taskId, int userId)
        {
            throw new System.NotImplementedException();
        }

        public List<WorkTask> GetAllTasks(int userId)
        {
            throw new System.NotImplementedException();
        }

        public int SaveTask(WorkTask task)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateTask(WorkTask task)
        {
            throw new System.NotImplementedException();
        }
    }
}