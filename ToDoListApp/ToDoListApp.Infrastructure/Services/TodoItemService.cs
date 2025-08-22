using ToDoListApp.Application.Abstractions.Repositories;
using ToDoListApp.Application.Abstractions.Services;
using ToDoListApp.Application.Abstractions.UnitOfWork;
using ToDoListApp.Application.DTOs;
using ToDoListApp.Domain.Enums;
using ToDoListApp.Domain.Models;

namespace ToDoListApp.Infrastructure.Services;

public class TodoItemService : ITodoItemService
{
    private IUnitOfWork _unitOfWork;
    private ITodoItemRepository _todoItemRepository;

    public TodoItemService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _todoItemRepository = _unitOfWork.TodoItemRepository;
    }

    public async Task<IEnumerable<GetTodoItemDTO>> GetAllAsync()
    {
        var items = await _todoItemRepository.GetAllAsync();
        return items.Where(item => item.IsDeleted == false)
                    .Select(item => new GetTodoItemDTO 
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        TodoStatus = item.TodoStatus,
                    });
    }

    public async Task<IEnumerable<GetTodoItemDTO>> GetAllDeletedAsync()
    {
        var items = await _todoItemRepository.GetAllAsync();
        return items.Where(item => item.IsDeleted == true)
                    .Select(item => new GetTodoItemDTO
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Description = item.Description,
                        TodoStatus = item.TodoStatus,
                    });
    }

    public async Task<GetTodoItemDTO> GetAsync(int id)
    {
        var item = await _todoItemRepository.GetAsync(id);
        if (item != null && !item.IsDeleted) 
            return new GetTodoItemDTO
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    TodoStatus = item.TodoStatus,
                };
        return null;
    }

    public async Task CreateAsync(CreateTodoItemDTO todoItem)
    {
        await _todoItemRepository.AddAsync(new TodoItem 
        {
            Name = todoItem.Name,
            Description = todoItem.Description,
            CreatedDate = DateTime.UtcNow,
        });
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _todoItemRepository.GetAsync(id);
        if (item == null || item.IsDeleted)
            return;
        item.IsDeleted = true;
        await _unitOfWork.CommitAsync();
    }

    public async Task RestoreAsync(int id)
    {
        var item = await _todoItemRepository.GetAsync(id);
        if (item != null && item.IsDeleted != false)
            item.IsDeleted = false;
            await _unitOfWork.CommitAsync();
    }

    public async Task UpdateAsync(UpdateTodoItemDTO updatedTodoItem)
    {
        var item = await _todoItemRepository.GetAsync(updatedTodoItem.Id);
        if (item == null || item.IsDeleted)
        {
            throw new KeyNotFoundException($"TodoItem with Id {updatedTodoItem.Id} not found or deleted.");
        }

        item.Name = updatedTodoItem.Name;
        item.Description = updatedTodoItem.Description;
        item.TodoStatus = updatedTodoItem.TodoStatus;
        item.UpdatedDate = DateTime.UtcNow;

        await _todoItemRepository.UpdateAsync(item);
        await _unitOfWork.CommitAsync();
    }

    public async Task ChangeStatusAsync(ChangeTodoStatusDTO changeTodoStatus)
    {
        var item = await _todoItemRepository.GetAsync(changeTodoStatus.Id);
        if (item != null && item.TodoStatus != changeTodoStatus.NewStatus)
            item.TodoStatus = changeTodoStatus.NewStatus;
            await _unitOfWork.CommitAsync();
    }
}
