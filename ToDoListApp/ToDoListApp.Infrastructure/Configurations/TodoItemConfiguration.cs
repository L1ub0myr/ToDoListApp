using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoListApp.Domain.Models;

namespace ToDoListApp.Infrastructure.Configurations;

public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(t => t.Description)
            .HasMaxLength(150);
        builder.Property(t => t.TodoStatus)
            .HasDefaultValue(TodoStatus.Todo);
        builder.Property(t => t.IsDeleted)
            .HasDefaultValue(false);
    }
}
