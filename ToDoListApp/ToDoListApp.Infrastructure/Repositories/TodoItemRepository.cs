using Microsoft.EntityFrameworkCore;
using ToDoListApp.Application.Interfaces;
using ToDoListApp.Domain.Models;

namespace ToDoListApp.Infrastructure.Repositories;

public class TodoItemRepository : ITodoItemRepository
{
    private TodoContext _dbContext;
    private DbSet<TodoItem> _dbSet;
    
    public TodoItemRepository(TodoContext context)
    {
        _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _dbContext.Set<TodoItem>();
    }

    public async Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<TodoItem> GetAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(TodoItem todoItem)
    {
        await _dbSet.AddAsync(todoItem);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(TodoItem todoItem)
    {
        _dbSet.Update(todoItem);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetAsync(id);
        if (entity != null)
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
    }
}
