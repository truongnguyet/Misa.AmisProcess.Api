using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MisaWebApi.Models;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Google.Protobuf.WellKnownTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Runtime.InteropServices;

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
        public ActionResult<Phase> GetPhaseId(string id)
        {

            var phase =  _context.Phase.Where(c => c.Id == id).Include(a => a.UsersHasPhase).SingleOrDefault();
            if (phase == null)
            {
                return NotFound();
            }

            return phase;
        }

        //GET ca field
        [HttpGet("{id}/get")]

        public ActionResult<Phase> Get(string id)
        {
            var item = _context.Phase.Where(c => c.Id == id)
                .Include(d => d.FieldData)
                .FirstOrDefault();
            return item;
        }
        //tạo phase bao gồm cả field, user và option
        [HttpPost("createAll")]
        public async Task<ActionResult<Phase>> Create(Phase phase)
        {
            
            var newPhase = new Phase
            {
                Id  =phase.Id,
                PhaseName = phase.PhaseName,
                Icon = phase.Icon,
                Description = phase.Description,
                IsFirstPhase = phase.IsFirstPhase,
                IsTb = phase.IsTb,
                IsTc = phase.IsTc,
                LimitUser = phase.LimitUser,
                ProcessId = phase.ProcessId,
                Index = phase.Index
            };

            foreach (var a in phase.FieldData)
            {
                var newField = new FieldData
                {
                    Id =a.Id,
                    FieldName = a.FieldName,
                    Description = a.Description,
                    Required = a.Required,
                    Type = a.Type,
                    PhaseId = newPhase.Id
                };
                _context.FieldData.AddRange(newField);
                foreach(var b in a.Option)
                {
                    var option = new Models.Option
                    {
                        Id = b.Id,
                        Value = b.Value,
                        FieldDataId = newField.Id
                    };
                    _context.Option.AddRange(option);
                }
            }
            foreach( var a in phase.UsersHasPhase)
            {
                var users = new UsersHasPhase
                {
                    UsersId = a.UsersId,
                    PhaseId = a.PhaseId
                };
                _context.UsersHasPhase.AddRange(users);
            }
            _context.Phase.Add(newPhase);
           await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPhaseId),
                new { id = newPhase.Id },
                newPhase);
        }




        // PUT: api/Phase/edit
        [HttpPut("edit")]
        public async Task<ActionResult<Phase>> Put([FromBody]Phase model)
        {

            var item = await _context.Phase.FirstOrDefaultAsync(x => x.Id.Equals(model.Id));

            if (item == null) return NotFound();

            item.PhaseName = model.PhaseName;
            item.Icon = model.Icon;
            item.Description = model.Description;
            item.LimitUser = model.LimitUser;
            await _context.SaveChangesAsync();
            return item;
        }
        // PUT: api/Phase/edit
        [HttpPut("editPhase")]
        public async Task<ActionResult<Phase>> PutPhase([FromBody] Phase model)
        {

            var item = await _context.Phase.FirstOrDefaultAsync(x => x.Id.Equals(model.Id));

            if (item == null) return NotFound();

            item.PhaseName = model.PhaseName;
            item.Icon = model.Icon;
            item.Description = model.Description;
           // item.LimitUser = model.LimitUser;

            await _context.SaveChangesAsync();
            return item;
        }

        // PUT: api/Phase/edit
        [HttpPut("editField")]
        public async Task<ActionResult<Phase>> PutField([FromBody] Phase model)
        {

            var item = await _context.Phase.FirstOrDefaultAsync(x => x.Id.Equals(model.Id));

            if (item == null) return NotFound();

            foreach (var a in model.FieldData)
            {
                // tim cai field id nay da ton tai chua
                var fieldItem = await _context.FieldData.FindAsync(a.Id);
                // kiem tra da ton tai thi update 
                if (fieldItem != null)
                {
                    // update
                    fieldItem.FieldName = a.FieldName;
                    fieldItem.Description = a.Description;
                    fieldItem.Type = a.Type;
                    fieldItem.Required = a.Required;
                    foreach (var b in a.Option)
                    {
                        var optionItem = await _context.Option.FindAsync(b.Id);
                        if (optionItem != null)
                        {
                            optionItem.Value = b.Value;
                        }
                        else
                        {
                            var newOption = new Models.Option
                            {
                                Id = b.Id,
                                Value = b.Value,
                                FieldDataId = b.FieldDataId
                            };
                            _context.Option.AddRange(newOption);
                        }
                    }
                }
                else
                {
                    // create new
                    var newField = new FieldData
                    {
                        Id = a.Id,
                        FieldName = a.FieldName,
                        Description = a.Description,
                        Type = a.Type,
                        Required = a.Required,
                        PhaseId = a.PhaseId,
                    };
                    foreach(var c in a.Option)
                    {
                        var newOption = new Models.Option
                        {
                            Id = c.Id,
                            Value = c.Value,
                            FieldDataId = c.FieldDataId
                        };
                        _context.Option.AddRange(newOption);
                    }
                    _context.FieldData.AddRange(newField);
                }
            }

            await _context.SaveChangesAsync();
            return item;
        }
        //Sửa user trong phase
        [HttpPut("editUser")]
        public async Task<ActionResult<Phase>> PutUser([FromBody] PhaseDelete model)
        {

            var item = await _context.Phase.FirstOrDefaultAsync(x => x.Id.Equals(model.Id));

            if (item == null) return NotFound();
            foreach(var a in model.UsersHasPhase)
            {
                var userItem =  _context.UsersHasPhase.Where( x => x.PhaseId == a.PhaseId && x.UsersId == a.UsersId);
                if(userItem == null)
                {
                    var newUserPhase = new UsersHasPhase
                    {
                        PhaseId = a.PhaseId,
                        UsersId = a.UsersId
                    };
                    _context.UsersHasPhase.Add(newUserPhase);
                }
            }
            foreach(var b in model.UserDelete)
            {
                var userDel = await _context.UsersHasPhase.FindAsync(b.UsersId, b.PhaseId);
                 _context.UsersHasPhase.Remove(userDel);
            }
           
            await _context.SaveChangesAsync();
            
            return item;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Phase>> DeleteTodoItem(Guid id)
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

        //Delete toàn bộ phase bao gồm cả field và user
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Phase>> Delete(string id)
        {
            var item = _context.Phase.Where(c => c.Id.Equals(id)).SingleOrDefault();
            if (item == null)
            {
                return NotFound();
            }
            var field = _context.FieldData.Where(d => d.PhaseId == item.Id).ToList();
            field.ForEach(d =>
           {
               var option = _context.Option.Where(a => a.FieldDataId == d.Id).ToList();
               _context.Option.RemoveRange(option);
           });
            _context.FieldData.RemoveRange(field);
            var users = _context.UsersHasPhase.Where(a => a.PhaseId == id).ToList();
            _context.UsersHasPhase.RemoveRange(users);
            _context.Phase.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

    }
}
