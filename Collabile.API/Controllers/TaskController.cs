using Collabile.DataAccess.Models;
using Collabile.Api.Services;
using Collabile.Shared.Constants;
using Collabile.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using Collabile.Shared.Models.Items;

namespace Collabile.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            string userId = User.FindFirstValue(ClaimTypes.Name);
            List<WorkTask> tasks = _taskService.GetAllTasks(int.Parse(userId));
            return Ok(tasks);
        }

        [HttpPost]
        public IActionResult Post(WorkTask task)
        {
            int taskId = _taskService.SaveTask(task);
            task.Id = taskId;
            return Ok(task);
        }

        [HttpPut]
        public IActionResult Put(WorkTask task)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            string role = User.FindFirstValue(ClaimTypes.Role);
            if (role == Role.Admin || task.Reporter == userId)
            {
                task.Reporter = userId;
                _taskService.UpdateTask(task);
                return Ok();
            }
            return BadRequest("Authorization issue");
        }

        [Route("{taskId}")]
        [HttpDelete]
        public void Delete(int taskId)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            _taskService.DeleteTask(taskId, userId);
        }
    }
}