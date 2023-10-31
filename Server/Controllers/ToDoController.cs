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
            var list = await _context.ToDos.ToListAsync();

        return Ok(list);
        }
    }
}
