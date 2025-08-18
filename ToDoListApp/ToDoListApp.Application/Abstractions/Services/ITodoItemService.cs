using ToDoListApp.Domain.Enums;
using ToDoListApp.Domain.Models;

namespace ToDoListApp.Application.Abstractions.Services;

public interface ITodoItemService
{
    Task<IEnumerable<TodoItem>> GetAllAsync();
    Task<IEnumerable<TodoItem>> GetAllDeletedAsync();
    Task<TodoItem> GetAsync(int id);
    Task CreateAsync(TodoItem todoItem);
    Task UpdateAsync(TodoItem todoItem);
    Task DeleteAsync(int id);
    Task RestoreAsync(int id);
    Task ChangeStatusAsync(int id, TodoStatus todoStatus);
}
