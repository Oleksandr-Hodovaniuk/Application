using CoWorking.Application.DTOs.Workspace;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Queries.Workspaces;

public record GetAllWorkspacesQuery() : IRequest<IEnumerable<WorkspaceDTO>>;
