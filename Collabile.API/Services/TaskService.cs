using Collabile.Api.DataAccess;
using Collabile.Shared.Entities;
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
            _ = _sql.SaveData("dbo.spTask_Delete", new { Id = taskId, userId }, "CollabileData");
        }

        public List<Task> GetAllTasks(int userId)
        {
            var tasks = _sql.LoadData<Task, dynamic>("dbo.spTask_GetAll", new { userId }, "CollabileData");
            return tasks;
        }

        public int SaveTask(Task task)
        {
            return _sql.SaveDataScalar("dbo.spTask_Insert", task, "CollabileData");
        }

        public void UpdateTask(Task task)
        {
            _ = _sql.SaveData("dbo.spTask_Update", task, "CollabileData");
        }
    }
}