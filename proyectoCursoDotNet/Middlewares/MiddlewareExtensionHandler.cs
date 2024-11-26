namespace proyectoCursoDotNet.Middlewares;

public static class MiddlewareExtensionHandler
{
    public static IApplicationBuilder UseMiddlewareExtensionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExcepcionHandlerMiddleWare>();
    }
}