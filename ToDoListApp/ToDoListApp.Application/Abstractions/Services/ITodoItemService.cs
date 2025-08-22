using ToDoListApp.Application.DTOs;

namespace ToDoListApp.Application.Abstractions.Services;

public interface ITodoItemService
{
    Task<IEnumerable<GetTodoItemDTO>> GetAllAsync();
    Task<IEnumerable<GetTodoItemDTO>> GetAllDeletedAsync();
    Task<GetTodoItemDTO> GetAsync(int id);
    Task CreateAsync(CreateTodoItemDTO todoItem);
    Task UpdateAsync(UpdateTodoItemDTO updatedTodoItem);
    Task DeleteAsync(int id);
    Task RestoreAsync(int id);
    Task ChangeStatusAsync(ChangeTodoStatusDTO changeTodoStatus);
}
