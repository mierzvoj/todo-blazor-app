using Microsoft.EntityFrameworkCore;
using Shared;

namespace BlazorToDoApp.Server.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options): base(options)
    {
        
    }
    public DbSet<Todo> ToDos { get; set; }
}