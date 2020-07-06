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
        public  ActionResult<FieldData> GetFieldId(string id)
        {

            var field =  _context.FieldData.Include(a => a.Option).Where(p => p.Id == id)
                .Include(d => d.Option)
                .FirstOrDefault();

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
                Id = field.Id,
                FieldName = field.FieldName,
                Description = field.Description,
                Type = field.Type,
                Required = field.Required,
                PhaseId = field.PhaseId
            };
            foreach (var a in field.Option)
            {
                var newOption = new Models.Option
                {
                    Id = a.Id,
                    Value = a.Value,
                    FieldDataId = a.FieldDataId
                };
                _context.Option.AddRange(newOption);
            }
            _context.FieldData.Add(newField);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetFieldId),
                new { id = newField.Id },
                newField);
        }

        // PUT: api/Field/5
        [HttpPut("edit")]
        public async Task<ActionResult<FieldData>> Put([FromBody]FieldData model)
        {

            var item = await _context.FieldData.FirstOrDefaultAsync(x => x.Id.Equals(model.Id));

            if (item == null) return NotFound();
            item.FieldName = model.FieldName;
            item.Description = model.Description;
            item.Required = model.Required;
            foreach( var a in model.Option)
            {
                var optionItem = await _context.Option.FindAsync(a.Id);
                if(optionItem != null)
                {
                    //update
                    optionItem.Value = a.Value;
                }else
                {
                    //create
                    var newOption = new Models.Option
                    {
                        FieldDataId = a.Id,
                        Value = a.Value,
                        Id = a.Id
                    };
                    _context.Option.AddRange(newOption);
                }
            }

            await _context.SaveChangesAsync();
            return item;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public  ActionResult<FieldData> DeleteTodoItem(string id)
        {
            var todoItem = _context.FieldData.Where(a => a.Id.Equals(id)).SingleOrDefault();
            var item = _context.Option.Where(d => d.FieldDataId == todoItem.Id).ToList();

            _context.Option.RemoveRange(item);
            _context.FieldData.Remove(todoItem);
             _context.SaveChangesAsync();

            return todoItem;
        }

    }
}
