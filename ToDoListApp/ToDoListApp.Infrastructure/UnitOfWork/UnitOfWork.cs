using ToDoListApp.Application.Abstractions.Repositories;
using ToDoListApp.Application.Abstractions.UnitOfWork;
using ToDoListApp.Infrastructure.Data;
using ToDoListApp.Infrastructure.Repositories;

namespace ToDoListApp.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private TodoContext _context;
    private bool _disposed = false;
    private ITodoItemRepository _todoRepository;

    public ITodoItemRepository TodoItemRepository => _todoRepository;

    public UnitOfWork(TodoContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _todoRepository = new TodoItemRepository(_context);
    }

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    { 
        if (!_disposed && disposing)
            _context.Dispose();
        _disposed = true;
    }
}
