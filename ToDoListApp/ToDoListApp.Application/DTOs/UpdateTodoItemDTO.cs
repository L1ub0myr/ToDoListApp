using ToDoListApp.Domain.Enums;

namespace ToDoListApp.Application.DTOs;

public class UpdateTodoItemDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TodoStatus TodoStatus { get; set; }
}
