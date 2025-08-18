using Microsoft.EntityFrameworkCore;
using ToDoListApp.Application.Abstractions.Repositories;
using ToDoListApp.Domain.Models;

namespace ToDoListApp.Infrastructure.Repositories;

public class TodoItemRepository : GenericRepository<TodoItem>, ITodoItemRepository
{
    public TodoItemRepository(DbContext context) : base(context)
    {
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetAsync(id);
        if (entity != null)
            entity.IsDeleted = true;
    }
}
