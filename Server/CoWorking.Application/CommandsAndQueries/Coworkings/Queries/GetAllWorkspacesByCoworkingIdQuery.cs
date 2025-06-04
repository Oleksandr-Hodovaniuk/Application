using CoWorking.Application.DTOs.Workspace;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Coworkings.Queries;

public record GetAllWorkspacesByCoworkingIdQuery(int coworkingId) : IRequest<IEnumerable<WorkspaceDTO>>;

