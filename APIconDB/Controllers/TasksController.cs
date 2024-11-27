using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIconDB.Context;
using APIconDB.Entities;
using APIconDB.Validators;


namespace APIconDB.Controllers
{
    [Route("api/tasks/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly TasksDbContext _context;

        public TasksController(TasksDbContext context)
        {
            _context = context;
        }

        // GET: api/Tasks/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tasks>> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            //if (Tasks == null)
            //{
            //    return NotFound();
           // }

            return task;
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTasks(int id, Tasks task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }

            _context.Entry(task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TasksExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<ActionResult<Tasks>> PostTasks(Tasks task)
        {
            var validator = new TasksValidator();
            var validationResult = await validator.ValidateAsync(task);
            var claimType = ClaimsPrincipal.Current?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (claimType != null)
            {
                var userId = User.FindFirstValue(claimType);
                // Asignar el ID del usuario a la tarea
                if (userId != null) task.UserId = int.Parse(userId);
            }

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            _context.Tasks.Add(task);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TasksExists(task.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTasks", new { id = task.Id }, task);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTasks(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TasksExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}