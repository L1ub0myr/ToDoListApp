using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using ToDoListApp.Application.Abstractions.Services;
using ToDoListApp.Application.Abstractions.UnitOfWork;
using ToDoListApp.Infrastructure.Data;
using ToDoListApp.Infrastructure.Services;
using ToDoListApp.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<ITodoItemService, TodoItemService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
