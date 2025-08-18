using ToDoListApp.Domain.Models;

namespace ToDoListApp.Application.Abstractions.Repositories;

public interface ITodoItemRepository : IGenericRepository<TodoItem>
{
    Task RestoreAsync(int id);
    Task<IEnumerable<TodoItem>> GetAllDeletedAsync();
}
