using chat.api.Dtos;
using chat.api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace chat.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: UserController/Details/5
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Details()
        {
            var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var user = await this._userService.GetUserById(userIdStr.ToString());
            return Ok(user);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<LoginResponse> Login(LoginDto body)
        {
            return await this._userService.Login(body);
        }

        // GET: UserController/Create
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> Register(CreateUserDto body)
        {
            return await this._userService.Register(body);
        }
    }
}
