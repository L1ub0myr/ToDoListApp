using ToDoListApp.Application.DTOs;
using ToDoListApp.Application.Interfaces;
using ToDoListApp.Application.Mapping;

namespace ToDoListApp.Application.Services;

public class TodoItemService : ITodoItemService
{
    private ITodoItemRepository _todoItemRepository;

    public TodoItemService(ITodoItemRepository todoRepository)
    {
        _todoItemRepository = todoRepository;
    }

    public async Task<IEnumerable<GetTodoItemDTO>> GetAllAsync()
    {
        var items = await _todoItemRepository.GetAllAsync();
        return items.Where(item => item.IsDeleted == false)
                    .Select(TodoItemMapper.ToGetDTO);
    }

    public async Task<IEnumerable<GetTodoItemDTO>> GetAllDeletedAsync()
    {
        var items = await _todoItemRepository.GetAllAsync();
        return items.Where(item => item.IsDeleted == true)
                    .Select(TodoItemMapper.ToGetDTO);
    }

    public async Task<GetTodoItemDTO> GetAsync(int id)
    {
        var item = await _todoItemRepository.GetAsync(id);
        if (item == null)
            throw new KeyNotFoundException("TodoItem not found");
        if (item.IsDeleted)
            throw new InvalidOperationException($"TodoItem with Id: {item.Id} deleted");
        return TodoItemMapper.ToGetDTO(item);
    }

    public async Task CreateAsync(CreateTodoItemDTO todoItem)
    {
        await _todoItemRepository.AddAsync(TodoItemMapper.FromCreateDTO(todoItem));
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _todoItemRepository.GetAsync(id);
        
        if (item == null)
            throw new KeyNotFoundException($"TodoItem with id: {id} not found");
        
        if (!item.IsDeleted)
        {
            item.IsDeleted = true;
            await _todoItemRepository.UpdateAsync(item);
        }
    }

    public async Task RestoreAsync(int id)
    {
        var item = await _todoItemRepository.GetAsync(id);
        if (item == null)
            throw new KeyNotFoundException($"TodoItem not found");
        if (!item.IsDeleted)
            throw new InvalidOperationException($"TodoItem with Id: {item.Id} is not deleted");

        item.IsDeleted = false;
        await _todoItemRepository.UpdateAsync(item);
    }

    public async Task UpdateAsync(UpdateTodoItemDTO updatedTodoItem)
    {
        var item = await _todoItemRepository.GetAsync(updatedTodoItem.Id);
        if (item == null)
        {
            throw new KeyNotFoundException($"TodoItem with Id: {updatedTodoItem.Id} not found.");
        }

        item.Name = updatedTodoItem.Name;
        item.Description = updatedTodoItem.Description;
        item.TodoStatus = updatedTodoItem.TodoStatus;
        item.UpdatedDate = DateTime.UtcNow;

        await _todoItemRepository.UpdateAsync(item);
    }

    public async Task ChangeStatusAsync(ChangeTodoStatusDTO changeTodoStatus)
    {
        var item = await _todoItemRepository.GetAsync(changeTodoStatus.Id);
        if (item == null)
            throw new KeyNotFoundException($"TodoItem with Id: {changeTodoStatus.Id} not found");

        if (item.TodoStatus != changeTodoStatus.NewStatus)
        {
            item.TodoStatus = changeTodoStatus.NewStatus;
            await _todoItemRepository.UpdateAsync(item);
        }
    }
}
