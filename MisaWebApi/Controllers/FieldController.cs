using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MisaWebApi.Models;

namespace MisaWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FieldController : ControllerBase
    {
        private readonly AmisContext _context;
        public FieldController(AmisContext context)
        {
            _context = context;
        }

        // GET: api/Field/5
        [HttpGet("{id}")]
        public  ActionResult<FieldData> GetFieldId(int id)
        {

            var field =  _context.FieldData.Include(a => a.Option).Where(p => p.Id == id).FirstOrDefault();

            if (field == null)
            {
                return NotFound();
            }

            return field;
        }

        // POST: api/Field
        [HttpPost("create")]
        public async Task<ActionResult<Field>> CreateTodoItem(FieldData field)
        {
            var newField = new FieldData
            {
                FieldName = field.FieldName,
                Description = field.Description,
                Type = field.Type,
                Required = field.Required,
                PhaseId = field.PhaseId

            };

            _context.FieldData.Add(newField);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetFieldId),
                new { id = newField.Id },
                newField);
        }

        // PUT: api/Field/5
        [HttpPut("{id}")]
        public async Task<ActionResult<FieldData>> Put(int id, [FromBody]FieldData model)
        {

            var item = await _context.FieldData.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (item == null) return NotFound();
            item.FieldName = model.FieldName;
            item.Description = model.Description;
            item.Required = model.Required;
            item.Option = model.Option;

            await _context.SaveChangesAsync();
            return item;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public  ActionResult<FieldData> DeleteTodoItem(int id)
        {
            var todoItem = _context.FieldData.Where(c => c.Id == id).FirstOrDefault();
            if (todoItem == null)
            {
                return NotFound();
            }
            var item = _context.Option.Where(d => d.FieldDataId == todoItem.Id).ToList();

            _context.Option.RemoveRange(item);
            _context.FieldData.Remove(todoItem);
             _context.SaveChangesAsync();

            return todoItem;
        }
    }
}
