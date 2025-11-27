using chat.api.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace chat.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {


        // GET: UserController/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        [HttpPost]
        public ActionResult<CreateUserDto> Create(CreateUserDto body)
        {
            return body;
        }
    }
}
