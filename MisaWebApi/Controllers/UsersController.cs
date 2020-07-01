using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MisaWebApi.Models;
using MisaWebApi.Services;
using MisaWebApi;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace MisaWebApi.Controllers
{
   // [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private  AmisContext _context;
        public UsersController(IUserService userService, AmisContext context)
        {
            _userService = userService;
            _context = context;
        }


        //[AllowAnonymous]
        //[HttpPost("login")]
        //public  IActionResult Login(Users model)
        //{

        //    //  var user = _userService.Authenticate(model.username, model.password);
        //    //var users = _context.Users.ToListAsync();

        //    //var result = await (from p in users
        //    //         where (p.username == model.username && p.password == model.password)
        //    //         select p
        //    //              )
        //    //             .ToListAsync();

        //    //if (user == null)
        //    //    return BadRequest(new { message = "username or password is incorrect" });
        //    return "Hello";
        //}

        // GET: api/Users
       
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("getUser")]
        public async Task<ActionResult<IEnumerable>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }


    }
}
