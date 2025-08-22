using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoListApp.Domain.Enums;
using ToDoListApp.Domain.Models;

namespace ToDoListApp.Infrastructure.Configurations;

public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(x => x.Description)
            .HasMaxLength(150);
        builder.Property(x => x.TodoStatus)
            .HasDefaultValue(TodoStatus.Todo);
        builder.Property(x => x.IsDeleted)
            .HasDefaultValue(false);
    }
}
