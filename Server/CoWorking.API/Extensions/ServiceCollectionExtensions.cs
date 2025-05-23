using CoWorking.API.Middlewares;

namespace CoWorking.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPresentation(this IServiceCollection services)
    {
        // Add controllers.
        services.AddControllers();;

        // Add Swagger.
        services.AddSwaggerGen();
    }
}
