using ToDoListApp.Application.Abstractions.Repositories;
using ToDoListApp.Application.Abstractions.Services;
using ToDoListApp.Application.Abstractions.UnitOfWork;
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

    public async Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        var items = await _todoItemRepository.GetAllAsync();
        return items.Where(item => item.IsDeleted == false);
    }

    public async Task<IEnumerable<TodoItem>> GetAllDeletedAsync()
    {
        var items = await _todoItemRepository.GetAllAsync();
        return items.Where(item => item.IsDeleted == true);
    }

    public async Task<TodoItem> GetAsync(int id)
    {
        var item = await _todoItemRepository.GetAsync(id);
        if (item != null && !item.IsDeleted) 
            return item;
        return null;
    }

    public async Task CreateAsync(TodoItem todoItem)
    {
        await _todoItemRepository.AddAsync(todoItem);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeleteAsync(int id)
    {
       await _todoItemRepository.DeleteAsync(id);
       await _unitOfWork.CommitAsync();
    }

    public async Task RestoreAsync(int id)
    {
        var item = await _todoItemRepository.GetAsync(id);
        if (item != null && item.IsDeleted != false)
            item.IsDeleted = false;
            await _unitOfWork.CommitAsync();
    }

    public async Task UpdateAsync(TodoItem todoItem)
    {
        await _todoItemRepository.UpdateAsync(todoItem);
        await _unitOfWork.CommitAsync();
    }

    public async Task ChangeStatusAsync(int id, TodoStatus todoStatus)
    {
        var item = await _todoItemRepository.GetAsync(id);
        if (item != null && item.TodoStatus != todoStatus)
            item.TodoStatus = todoStatus;
            await _unitOfWork.CommitAsync();
    }
}
