using ToDoListApp.Domain.Enums;
using ToDoListApp.Domain.Models;

namespace ToDoListApp.Application.Abstractions.Repositories;

public interface ITodoItemRepository : IGenericRepository<TodoItem>
{
    
}
