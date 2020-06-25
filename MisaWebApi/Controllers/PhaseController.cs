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
    public class PhaseController : ControllerBase
    {
        private readonly AmisContext _context;

        public PhaseController(AmisContext context)
        {
            _context = context;
        }
        // GET: api/Phase
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
