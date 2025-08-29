using ToDoListApp.Domain.Models;

namespace ToDoListApp.Application.Interfaces;

public interface ITodoItemRepository
{
    Task<IEnumerable<TodoItem>> GetAllAsync();
    Task<TodoItem> GetAsync(int id);
    Task AddAsync(TodoItem todoItem);
    Task UpdateAsync(TodoItem todoItem);
    Task DeleteAsync(int id);

}
