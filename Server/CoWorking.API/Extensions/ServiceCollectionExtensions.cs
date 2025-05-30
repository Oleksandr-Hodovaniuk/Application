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

        // Configure CORS to allow requests from the Angular frontend
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularApp",
                policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
        });
    }
}
