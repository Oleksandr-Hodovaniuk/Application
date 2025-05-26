using CoWorking.Application.DTOs.Workspace;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Workspaces.Queries;

public record GetWorkspacesInfoQuery : IRequest<IEnumerable<DropDownWorkspaceDTO>>;

