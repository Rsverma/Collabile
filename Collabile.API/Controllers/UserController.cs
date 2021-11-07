using Collabile.Shared.Entities;
using Collabile.Api.Services;
using Collabile.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Collabile.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public IActionResult Post(User user)
        {
            user = _userService.CreateUser(user);
            if (user == null)
                return BadRequest();
            return Ok(user);
        }

        [HttpPut]
        public IActionResult Put(User user)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            string role = User.FindFirstValue(ClaimTypes.Role);
            if (role == Role.Admin || user.Id == userId)
            {
                _userService.UpdateUser(user);
                return Ok();
            }
            return BadRequest("Authorization issue");
        }

        [Route("{userId}")]
        [HttpDelete]
        public IActionResult Delete(int userId)
        {
            int currentUserId = int.Parse(User.FindFirstValue(ClaimTypes.Name));
            string role = User.FindFirstValue(ClaimTypes.Role);
            if (role == Role.Admin || userId == currentUserId)
            {
                _userService.DeleteUser(userId);
                return Ok();
            }
            return BadRequest("Authorization issue");
        }
    }
}