using ToDoListApp.Application.DTOs;
using ToDoListApp.Domain.Models;

namespace ToDoListApp.Infrastructure.Mapping;

public static class TodoItemMapper
{
    public static GetTodoItemDTO ToGetDTO(TodoItem todoItem)
    {
        return new GetTodoItemDTO
        {
            Id = todoItem.Id,
            Name = todoItem.Name,
            Description = todoItem.Description,
            TodoStatus = todoItem.TodoStatus,
        };
    }

    public static TodoItem FromCreateDTO(CreateTodoItemDTO createTodoItem)
    {
        return new TodoItem
        {
            Id = createTodoItem.Id,
            Name = createTodoItem.Name,
            Description = createTodoItem.Description,
            CreatedDate = DateTime.UtcNow
        };
    }
}
