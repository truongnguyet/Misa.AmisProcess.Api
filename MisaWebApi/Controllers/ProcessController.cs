﻿using System;
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
        public async Task<ActionResult<Process>> GetProcessId(int id)
        {
            var todoItem = await _context.Process.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        // POST: api/Process
        [HttpPost("create")]
        public async Task<ActionResult<Process>> CreateTodoItem(Process process)
        {
            var todoItem = new Process
            {
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

        // PUT: api/Process/5

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Process model)
        {

            var item = await _context.Process.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (item == null) return NotFound();

            item.NameProcess = model.NameProcess;
            item.Status = model.Status;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE:Process/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Process>> DeleteTodoItem(int id)
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
        private bool ProcessExist(int id) =>
       _context.Process.Any(e => e.Id == id);

    }
}