using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Entities;

namespace WebApi.Controllers
{
  [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = _userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll(string token)
        {
            var users =  _userService.GetAll(token);
            return Ok(users);
        }
        [HttpGet]
        public IActionResult GetUser(string Username)
        {
            var users =  _userService.GetUser(Username);
            if(users == null)
                return Ok("no user found");
            return Ok(users);
        }
        [HttpDelete]
        public IActionResult DeleteUser(string Username)
        {
            var users =  _userService.DeleteUser(Username);
            return Ok(users);
        }
        [HttpPost]
        public IActionResult UpdateUser([FromBody]User userParam)
        {
           var users = _userService.UpdateUser(userParam);
           return Ok(users);
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateUser([FromBody]User userParam)
        {
            userParam.Username = userParam.MobileNumber;
            var users = _userService.CreateUser(userParam);
            if(!users.IsError)
            {
                var user = _userService.Authenticate(userParam.Username, userParam.Password);
                return Ok(user);    
            }
            
            return Ok(users);
        }
    }
}
