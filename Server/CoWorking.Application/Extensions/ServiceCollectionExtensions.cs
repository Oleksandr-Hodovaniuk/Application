using CoWorking.Application.CommandsAndQueries.Workspaces.Handlers;
using CoWorking.Application.Mappings;
using CoWorking.Application.Models;
using CoWorking.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoWorking.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        // Registration of AutoMappers.
        services.AddAutoMapper(typeof(WorkspaceProfile).Assembly);

        // Registration of MediatR (queries, commands, handlers).
        services.AddMediatR(config => {
            config.RegisterServicesFromAssembly(typeof(GetAllWorkspacesHandler).Assembly);
        });

        // Registration of validators.
        services.AddValidatorsFromAssemblyContaining<CreateBookingDTOValidator>();

        // Binds the "AiAssistant" section from appsettings.json.
        services.Configure<AiAssistantSettings>(configuration.GetSection("AiAssistant"));
    }
}
