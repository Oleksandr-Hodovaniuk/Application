using CoWorking.Application.DTOs.Workspace;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Workspaces.Queries;

public record GetAllWorkspacesQuery() : IRequest<IEnumerable<WorkspaceDTO>>;
