using Microsoft.AspNetCore.Mvc;
using ToDoListApp.Application.DTOs;
using ToDoListApp.Application.Interfaces;

namespace ToDoListApp.WebApi.Controllers;

[ApiController]
[Route("api/todos")]
public class TodoItemController : ControllerBase
{
    private ITodoItemService _todoService;

    public TodoItemController(ITodoItemService todoService) 
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetTodoItemDTO>>> Get([FromQuery] bool isDeleted = false)
    { 
        if (!isDeleted)
            return Ok(await _todoService.GetAllAsync());
        return Ok(await _todoService.GetAllDeletedAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<GetTodoItemDTO>>> Get([FromRoute] int id)
    {
        var item = await _todoService.GetAsync(id);
        if (item == null) 
            return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateTodoItemDTO createTodoItem)
    { 
        await _todoService.CreateAsync(createTodoItem);
        return CreatedAtAction(nameof(Get), new { Id =createTodoItem.Id }, createTodoItem);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateTodoItemDTO updateTodoItem)
    {
        await _todoService.UpdateAsync(updateTodoItem);
        return NoContent();
    }

    [HttpPatch]
    public async Task<IActionResult> ChangeStatus(ChangeTodoStatusDTO changeTodoStatus)
    {
        await _todoService.ChangeStatusAsync(changeTodoStatus);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    { 
        await _todoService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPatch("restore/{id}")]
    public async Task<IActionResult> Restore([FromRoute] int id)
    {
        await _todoService.RestoreAsync(id);
        return NoContent();
    }
}
