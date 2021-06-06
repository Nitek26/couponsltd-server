using CouponsLtd.Services;
using CouponsLtd.UpsertModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CouponsLtd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("createmockusers")]
        public async Task<IActionResult> CreateMockUsers(UserUpsert user)
        {
            var response = await _userService.Create(user);

            if (response==null)
                return BadRequest(new { message = "Something went wrong" });

            return Ok(response);
        }
    }
}
