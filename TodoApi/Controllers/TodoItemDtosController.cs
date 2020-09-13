using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemDtosController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemDtosController(TodoContext context)
        {
            _context = context;
        }

        // GET: api/TodoItems
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()            //MODIFIED BY dr FOR DTO
        {
            //return await _context.TodoItems.ToListAsync();
            return await _context.TodoItems.Select(x => ItemToDTO(x)).ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)               //MODIFIED BY dr FOR DTO
        {
            var todoItem = await _context.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            //return todoItem;
            return ItemToDTO(todoItem);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        //public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        public async Task<IActionResult> PutTodoItem(long id, TodoItemDTO todoItemDTO)          //MODIFIED BY dr FOR DTO
        {
            //if (id != todoItem.Id)
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }
            var todoItem = await _context.TodoItems.FindAsync(id);                              //ADDED BY dr FOR DTO
            if (todoItem == null)
            {
                return NotFound();
            }
            todoItem.Name = todoItemDTO.Name;                                                   //ADDED BY dr FOR DTO
            todoItem.IsComplete = todoItemDTO.IsComplete;

            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(id))
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

        // POST: api/TodoItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        //public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItemDTO todoItemDTO)          //MODIFIED BY dr FOR DTO
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);         //MODIFIED BY dr TO AVOID HARD-CODING (DEFAULT)
            //return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, ItemToDTO(todoItem));    //MODIFIED BY dr FOR DTO
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        //public async Task<ActionResult<TodoItemDTO>> DeleteTodoItem(long id)
        public async Task<ActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            ////return todoItem;
            //return ItemToDTO(todoItem);
            return NoContent();
        }

        private bool TodoItemExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>                      //ADDED BY dr FOR DTO
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}
