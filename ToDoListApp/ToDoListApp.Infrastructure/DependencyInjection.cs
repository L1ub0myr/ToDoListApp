
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoListApp.Application.Interfaces;
using ToDoListApp.Application.Services;
using ToDoListApp.Infrastructure.Repositories;

namespace ToDoListApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrasructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TodoContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<ITodoItemRepository, TodoItemRepository>();
        services.AddScoped<ITodoItemService, TodoItemService>();
        
        return services;
    }
}
