using AutoMapper;
using CoWorking.Application.CommandsAndQueries.Coworkings.Queries;
using CoWorking.Application.DTOs.Workspace;
using CoWorking.Application.Interfaces.Repositories;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Coworkings.Handlers;

public class GetAllWorkspacesByCoworkingIdHandler : IRequestHandler<GetAllWorkspacesByCoworkingIdQuery, IEnumerable<WorkspaceDTO>>
{
    private readonly ICoworkingRepository _repository;
    private readonly IMapper _mapper;
    public GetAllWorkspacesByCoworkingIdHandler(ICoworkingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<WorkspaceDTO>> Handle(GetAllWorkspacesByCoworkingIdQuery request, CancellationToken cancellationToken)
    {
        var workspaces = await _repository.GetAllWorkspacesByCoworkingIdAsync(request.coworkingId, cancellationToken);

        if (!workspaces.Any())
        {
            return Enumerable.Empty<WorkspaceDTO>();
        }

        return _mapper.Map<IEnumerable<WorkspaceDTO>>(workspaces);
    }
}
