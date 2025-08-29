using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListApp.Domain.Models;

namespace ToDoListApp.Application.DTOs;

public class ChangeTodoStatusDTO
{
    public int Id { get; set; }
    public TodoStatus NewStatus { get; set; }
}
