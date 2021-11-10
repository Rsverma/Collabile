using Collabile.Shared.Entities;
using Collabile.Api.Services;
using Collabile.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Collabile.Api.Models;
using Collabile.Api.Helpers;

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

        [AuthorizeRoles(Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [AuthorizeRoles(Role.Admin)]
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
            string username = User.FindFirstValue(ClaimTypes.Name);
            string role = User.FindFirstValue(ClaimTypes.Role);
            if (role == Role.Admin.ToString() || user.Username == username)
            {
                _userService.UpdateUser(user);
                return Ok();
            }
            return BadRequest("Authorization issue");
        }

        [Route("{userId}")]
        [HttpDelete]
        public IActionResult Delete(string username)
        {
            string currentUsername = User.FindFirstValue(ClaimTypes.Name);
            string role = User.FindFirstValue(ClaimTypes.Role);
            if (role == Role.Admin.ToString() || username == currentUsername)
            {
                _userService.DeleteUser(username);
                return Ok();
            }
            return BadRequest("Authorization issue");
        }
    }
}