using ToDoListApp.Domain.Enums;

namespace ToDoListApp.Domain.Models;

public class TodoItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TodoStatus TodoStatus { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
