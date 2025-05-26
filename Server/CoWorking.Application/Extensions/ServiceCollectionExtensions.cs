using CoWorking.Application.CommandsAndQueries.Workspaces.Handlers;
using CoWorking.Application.Mappings;
using CoWorking.Application.Validators;
using FluentValidation;
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

        // Registration of validators.
        services.AddValidatorsFromAssemblyContaining<CreateBookingDTOValidator>();
    }
}
