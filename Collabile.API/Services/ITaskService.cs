using Collabile.Api.Models;
using System.Collections.Generic;

namespace Collabile.Api.Services
{
    public interface ITaskService
    {
        int SaveTask(WorkTask task);

        void UpdateTask(WorkTask task);

        void DeleteTask(int taskId, int userId);

        List<WorkTask> GetAllTasks(int userId);
    }
}