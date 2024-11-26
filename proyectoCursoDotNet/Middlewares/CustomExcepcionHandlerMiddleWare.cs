using System.Net;

namespace proyectoCursoDotNet.Middlewares;

public class CustomExcepcionHandlerMiddleWare(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var configuration = context.RequestServices.GetRequiredService<IConfiguration>();
        var isInMaintenanceMode = configuration.GetValue<bool>("MaintenanceMode:IsInMaintenance");

        if(isInMaintenanceMode && context.Request.Path != "/maintenance"){
            context.Response.Redirect("/maintenance");
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
                case InvalidOperationException e:
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