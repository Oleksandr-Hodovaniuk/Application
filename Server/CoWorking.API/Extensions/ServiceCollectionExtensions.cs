using CoWorking.API.Middlewares;
using CoWorking.API.Services;

namespace CoWorking.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPresentation(this IServiceCollection services)
    {
        // Add controllers and support for JSON Patch.
        services.AddControllers()
            .AddNewtonsoftJson();

        // Add Swagger.
        services.AddSwaggerGen();

        // Add background service to delete expired bookings.
        services.AddHostedService<BookingDeletionService>();
    }
}
