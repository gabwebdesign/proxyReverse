using System.Net;

namespace proyectoCursoDotNet.Middlewares;

public class CustomExcepcionHandlerMiddleWare(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var isInMaintenanceMode = context.RequestServices.GetRequiredService<IConfiguration>().GetValue<bool>("isInMaintenanceMode");
        
        if(isInMaintenanceMode && context.Request.Path != "/maintenance"){
            context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
            await context.Response.WriteAsync("We are in maintenance mode. Please try again later.");
        }
                    
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            switch (exception)
            {
                
                case KeyNotFoundException e:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case UnauthorizedAccessException e:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                case InvalidOperationException e:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case InvalidTimeZoneException e:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case NotImplementedException e:
                    context.Response.StatusCode = (int)HttpStatusCode.NotImplemented;
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            await context.Response.WriteAsync($"{exception.Message} using Middleware");
        }
    }
}