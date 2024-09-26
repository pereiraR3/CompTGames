namespace web_api_dakota.Middleware;

public class GlobalHandlerException
{
    private readonly RequestDelegate _next;

    public GlobalHandlerException(RequestDelegate next)
    {
        _next = next;
    }
    
    
    
}