using Microsoft.EntityFrameworkCore;
using ToDoListApp.Domain.Models;

namespace ToDoListApp.Infrastructure.Data;

public class TodoContext : DbContext
{
    DbSet<TodoItem> TodoItems { get; set; }

    public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoContext).Assembly);
    }
}
 