using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisaWebApi.Models;

namespace MisaWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        private readonly AmisContext _context;

        public TestController(AmisContext context)
        {
            _context = context;
        }

        // GET: api/Test
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //GET: 
        [HttpGet("hello")]
        public string Hello()
        {
            return "Hello from the api";
        }


        //GET: Test/2
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable>> GetTodoItem(long id)
        {
            return await _context.Process.ToListAsync();
        }

        // POST: api/Test
        [HttpPost("create")]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Test/5
        [HttpPut("edit/{id}")]
        


        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
