using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MisaWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MisaWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {

        private readonly AmisContext _context;

        public ProcessController(AmisContext context)
        {
            _context = context;
        }
        // GET all Process
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable>> Get()
        {
            return await _context.Process.ToListAsync();
        }


        // GET: Process/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Process>> GetProcessId(Guid id)
        {

            var todoItem = await _context.Process.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        //Get ca phase
        [HttpGet("phase/{id}")]
        public ActionResult<Process> GetProcess(string id)
        {

            var item = _context.Process.Where(p => p.Id == id)
                .Include(c => c.Phase)
                .FirstOrDefault();
            return item;

        }

        //Get ca phase va field
        [HttpGet("{id}/get")]
        public ActionResult<Process> Get (string id)
        {
            var item = _context.Process.Where(p => p.Id.Equals(id))
                .Include(ph => ph.Phase)
                .ThenInclude(c => c.FieldData)
                .ThenInclude(o => o.Option)
                .SingleOrDefault();
           
            return item;
        }

        // POST: api/Process
        [HttpPost("create")]
        public async Task<ActionResult<Process>> CreateTodoItem(Process process)
        {
            var todoItem = new Process
            {
                Id = process.Id,
                NameProcess = process.NameProcess,
                Status = process.Status,
                CreatedAt = process.CreatedAt,
                CreatedBy = process.CreatedBy,
                ModifiedBy = process.ModifiedBy,
                ModifiedAt = process.ModifiedAt

            };

            _context.Process.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetProcessId),
                new { id = todoItem.Id },
                todoItem);
        }

        // PUT

        [HttpPut("edit")]
        public async Task<ActionResult<Process>> Put([FromBody]Process model)
        {

            var item = await _context.Process.FirstOrDefaultAsync(x => x.Id.Equals(model.Id));

            if (item == null) return NotFound();
            item.NameProcess = model.NameProcess;
            item.Status = model.Status;

            _context.Process.Update(item);
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;

        }



        // DELETE:Process/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Process>> DeleteTodoItem(string id)
        {
            var todoItem = await _context.Process.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.Process.Remove(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }
        private bool ProcessExist(string id) =>
       _context.Process.Any(e => e.Id == id);

    }
}
