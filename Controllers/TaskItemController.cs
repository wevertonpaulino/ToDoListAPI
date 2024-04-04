using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ToDoListAPI.Data;
using ToDoListAPI.Models;

namespace ToDoListAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemController : ControllerBase
    {
        private readonly TaskContext _context;

        public TaskItemController(TaskContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var taskItems = await _context.TaskItems.ToListAsync();

            return Ok(taskItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);

            if (taskItem == null)
            {
                return NotFound();
            }

            return Ok(taskItem);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskItem taskItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TaskItems.Add(taskItem);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = taskItem.Id }, taskItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskItem taskItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var taskItemToUpdate = await _context.TaskItems.FindAsync(id);

            if (taskItemToUpdate == null)
            {
                return NotFound();
            }

            taskItemToUpdate.Title = taskItem.Title;
            taskItemToUpdate.Description = taskItem.Description;
            taskItemToUpdate.CompletedAt = taskItem.CompletedAt;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var taskItemToRemove = await _context.TaskItems.FindAsync(id);

            if (taskItemToRemove == null)
            {
                return NotFound();
            }

            _context.TaskItems.Remove(taskItemToRemove);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
