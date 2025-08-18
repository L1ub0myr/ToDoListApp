using ToDoListApp.Application.Abstractions.Repositories;

namespace ToDoListApp.Application.Abstractions.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    ITodoItemRepository TodoItemRepository { get; }
    Task<int> CommitAsync();
}
