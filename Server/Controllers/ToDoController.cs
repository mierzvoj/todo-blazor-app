using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BlazorToDoApp.Server.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly DataContext _context;
        public ToDoController(DataContext context)
        {
            _context = context;            
        }
        public async Task<ActionResult<List<Todo>>> GetAllTodos()
        {
            var list = await _context.ToDos.OrderBy(g => g.Date).ToListAsync();

        return Ok(list);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(int id)
        {
           var dbToDo = await _context.ToDos.FindAsync(id);
            if(dbToDo is null)
            {
                return NotFound("This todo is non existent");
            }

        return Ok(dbToDo);
        }

        [HttpPost]
        public async Task<ActionResult<List<Todo>>> CreateToDo(Todo todo)
        {
            _context.ToDos.Add(todo);
            await _context.SaveChangesAsync();
            
            return await GetAllTodos();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Todo>>> UpdateToDo(int id, Todo todo)
        {
            var dbToDo = await _context.ToDos.FindAsync(id);
            if(dbToDo is null)
            {
                return NotFound("This todo is non existent");
            }
            dbToDo.Name = todo.Name;
            dbToDo.Description = todo.Description;
            dbToDo.Date = todo.Date;


            await _context.SaveChangesAsync();
            
            return await GetAllTodos();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Todo>>> DeleteToDo(int id)
        {
            var dbToDo = await _context.ToDos.FindAsync(id);
            if(dbToDo is null)
            {
                return NotFound("This todo is non existent");
            }

           _context.ToDos.Remove(dbToDo);
           await _context.SaveChangesAsync();
            
            return await GetAllTodos();
        }
    }
}
