using Collabile.Shared.Entities;
using System.Collections.Generic;

namespace Collabile.Api.Services
{
    public interface ITaskService
    {
        int SaveTask(Task task);

        void UpdateTask(Task task);

        void DeleteTask(int taskId, int userId);

        List<Task> GetAllTasks(int userId);
    }
}