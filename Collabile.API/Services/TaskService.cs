using Collabile.Api.DataAccess;
using Collabile.Api.Models;
using Collabile.Shared.Models;
using System.Collections.Generic;

namespace Collabile.Api.Services
{
    public class TaskService : ITaskService
    {
        private readonly ISqlDataAccess _sql;

        public TaskService(ISqlDataAccess sql)
        {
            _sql = sql;
        }

        public void DeleteTask(int taskId, int userId)
        {
            _ = _sql.SaveDataAsync("dbo.spTask_Delete", new { Id = taskId, userId });
        }

        public List<WorkTask> GetAllTasks(int userId)
        {
            var tasks = _sql.LoadData<WorkTask, dynamic>("dbo.spTask_GetAll", new { userId });
            return tasks;
        }

        public int SaveTask(WorkTask task)
        {
            return _sql.SaveDataScalar("dbo.spTask_Insert", task);
        }

        public void UpdateTask(WorkTask task)
        {
            _ = _sql.SaveDataAsync("dbo.spTask_Update", task);
        }
    }
}