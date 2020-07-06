using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MisaWebApi.Models;

namespace MisaWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        private readonly AmisContext _context;

        public OptionController(AmisContext context)
        {
            _context = context;

        }
        //Xóa một option
        [HttpDelete]
        public async Task<ActionResult<Option>> DeleteTodoItem([FromBody] Option option)
        {
            var todoItem = await _context.Option.FindAsync(option.Id);
            if(todoItem == null)
            {
                return NotFound();
            }
            _context.Option.Remove(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

    }
}