using Bookings.Models.DB;
using Bookings.Services.UserServices;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Bookings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _service.GetAllUsers();
            if (users.Any())
                return Ok(users);
            return NotFound();
        }



        [HttpGet]
        [Route("GetUserById")]
        public IActionResult GetUserById(int? v)
        {
            if (v is null)
                return BadRequest();
            var user = _service.GetUserById(v);
            if (user is null)
                return NotFound();
            return Ok(user);
        }


        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (user is null)
                return NoContent();
            var result = await _service.CreateUser(user);
            return Created("", result);
        }
    }
}
