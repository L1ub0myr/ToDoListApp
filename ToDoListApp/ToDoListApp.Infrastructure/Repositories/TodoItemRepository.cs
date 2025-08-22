using Microsoft.EntityFrameworkCore;
using ToDoListApp.Application.Abstractions.Repositories;
using ToDoListApp.Domain.Models;

namespace ToDoListApp.Infrastructure.Repositories;

public class TodoItemRepository : GenericRepository<TodoItem>, ITodoItemRepository
{
    public TodoItemRepository(DbContext context) : base(context)
    {
    }
}
