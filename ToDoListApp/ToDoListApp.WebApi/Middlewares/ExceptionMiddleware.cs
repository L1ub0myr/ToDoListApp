namespace ToDoListApp.WebApi.Middlewares;

public class ExceptionMiddleware
{
    private RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    { 
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (KeyNotFoundException ex)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(ex.Message);
        }
    }
}
