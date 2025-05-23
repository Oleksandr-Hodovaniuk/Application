using CoWorking.Application.DTOs;
using MediatR;

namespace CoWorking.Application.Workspaces.Queries;

public record GetAllWorkspacesQuery() : IRequest<IEnumerable<WorkspaceDTO>>;
