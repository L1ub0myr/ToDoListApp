using Microsoft.EntityFrameworkCore;
using ToDoListApp.Application.Abstractions.Repositories;

namespace ToDoListApp.Infrastructure.Repositories;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected DbContext _dbContext;
    protected DbSet<T> _dbSet;

    public GenericRepository(DbContext context)
    { 
        _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _dbContext.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetAsync(id);
        if (entity != null)
            _dbSet.Remove(entity);
    }
}
