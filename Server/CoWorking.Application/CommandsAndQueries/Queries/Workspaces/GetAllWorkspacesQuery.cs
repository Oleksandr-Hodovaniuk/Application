using CoWorking.Application.DTOs;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Queries.Workspaces;

public record GetAllWorkspacesQuery() : IRequest<IEnumerable<WorkspaceDTO>>;
