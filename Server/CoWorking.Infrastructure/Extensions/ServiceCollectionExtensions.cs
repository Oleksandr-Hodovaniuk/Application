using CoWorking.Application.Interfaces.Repositories;
using CoWorking.Application.Interfaces.Seeders;
using CoWorking.Infrastructure.Persistence;
using CoWorking.Infrastructure.Persistence.DataSeeders;
using CoWorking.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoWorking.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Database connection.
        var connectionString = configuration.GetConnectionString("CoWorkingDb");
        services.AddDbContext<CoWorkingDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Registration of data seeder.
        services.AddScoped<ISeeder, DefaultSeeder>();

        // Registration of repositories.
        services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
    }
}
