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

        //Phân trang
        [HttpGet("page/{index}")]
        public ActionResult<Page> getProcessPagination(int index)
        {
            var perPage = 10;
            var countDetails = _context.Process.Count();
            var item = _context.Process.Skip(perPage * index).Take(perPage).ToList();
            var result = new Page
            {
                Count = countDetails,
                PageIndex = index,
                PageSize = perPage,
                Items = item
            };
          
            return result;
        }
        //Seacrh and pagination
        [HttpGet("{index}/{searchText}")]
        public ActionResult<Page> ProcessSearch(int index,string searchText)
        {
            var perPage = 10;
            var item = _context.Process.Where(x => x.NameProcess.ToUpper().Contains(searchText.ToUpper())).ToList();
            var todoItem =  item.Skip(perPage * index).Take(perPage).ToList();   
            var countDetails = item.Count();

            var result = new Page
            {
                Count = countDetails,
                PageIndex = 1,
                PageSize = perPage,
                Items = todoItem
            };

            return result;
        }

        //Get ca phase va field,user
        [HttpGet("{id}/get")]
        public ActionResult<Process> Get (string id)
        {
            var item = _context.Process.Where(p => p.Id.Equals(id))
                .Include(ph => ph.Phase)
                .ThenInclude(c => c.FieldData)
                .ThenInclude(o => o.Option)
                .SingleOrDefault();
            
            foreach( var a in item.Phase)
            {
                var users = _context.UsersHasPhase.Where(b => b.PhaseId == a.Id).ToArray();
                a.UsersHasPhase = users;
            }
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
                nameof(Get),
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
            item.ModifiedBy = model.ModifiedBy;
            item.ModifiedAt = model.ModifiedAt;

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

    //Get : search tên process

}
