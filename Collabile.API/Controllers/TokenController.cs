using Collabile.Api.Models;
using Collabile.Api.Services;
using Collabile.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Collabile.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IUserService _userService;

        public TokenController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest("Username or password is incorrect");

            return Ok(user);
        }

        [HttpPost("signup")]
        public IActionResult SignUp([FromBody] SignUpModel userDetails)
        {
            User newUser = new()
            {
                Username = userDetails.Username,
                Password = userDetails.Password,
                UserRole = Role.User.ToString()
            };
            User user = _userService.CreateUser(newUser);

            if (user == null)
                return BadRequest();

            return Ok(user);
        }
    }
}