using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MisaWebApi.Models;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace MisaWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PhaseController : ControllerBase
    {
        private readonly AmisContext _context;

        public PhaseController(AmisContext context)
        {
            _context = context;
        }
      

        // GET: api/Phase/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Phase>> GetPhaseId(int id)
        {

            var phase = await _context.Phase.FindAsync(id);
            if (phase == null)
            {
                return NotFound();
            }

            return phase;
        }

        //GET ca field
        [HttpGet("{id}/get")]
         
        public ActionResult<Phase> Get(int id)
        {
            var item = _context.Phase.Where(c => c.Id == id)
                .Include(d => d.FieldData)
                .FirstOrDefault();
            return item;
        }
        // POST: /Phase
        [HttpPost("create")]
        public async Task<ActionResult<Phase>> CreateTodoItem(Phase phase)
        {
            var newPhase = new Phase
            {
                PhaseName = phase.PhaseName,
                Icon = phase.Icon,
                Description = phase.Description,
                IsFirstPhase = phase.IsFirstPhase,
                IsTb = phase.IsTb,
                IsTc = phase.IsTc,
                LimitUser = phase.LimitUser,
                ProcessId = phase.ProcessId,

            };

            _context.Phase.Add(newPhase);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPhaseId),
                new { id = newPhase.Id },
                newPhase);
        }
     

        // PUT: api/Phase/5
        [HttpPut("edit/{id}")]
        public async Task<ActionResult<Phase>> Put(int id, [FromBody]Phase model)
        {

            var item = await _context.Phase.FirstOrDefaultAsync(x => x.Id.Equals(id));
        
            if (item == null) return NotFound();

            item.PhaseName = model.PhaseName;
            item.Icon = model.Icon;
            item.Description = model.Description;
            item.LimitUser = model.LimitUser;
            
            await _context.SaveChangesAsync();
            return item;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Phase>> DeleteTodoItem(int id)
        {
            var todoItem = await _context.Phase.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.Phase.Remove(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }
    }
}
