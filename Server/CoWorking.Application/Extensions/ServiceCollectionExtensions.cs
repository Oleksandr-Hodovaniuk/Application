using CoWorking.Application.CommandsAndQueries.Hendlers.Workspaces;
using CoWorking.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace CoWorking.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        // Registration of AutoMappers.
        services.AddAutoMapper(typeof(WorkspaceProfile).Assembly);

        // Registration of MediatR (queries, commands, handlers).
        services.AddMediatR(config => {
            config.RegisterServicesFromAssembly(typeof(GetAllWorkspacesHandler).Assembly);
        });
    }
}
