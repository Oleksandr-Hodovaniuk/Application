using AutoMapper;
using CoWorking.Application.CommandsAndQueries.Workspaces.Queries;
using CoWorking.Application.DTOs.Workspace;
using CoWorking.Application.Interfaces.Repositories;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Workspaces.Handlers;

public class GetAllWorkspacesHandler : IRequestHandler<GetAllWorkspacesQuery, IEnumerable<WorkspaceDTO>>
{
    private readonly IWorkspaceRepository _repository;
    private readonly IMapper _mapper;
    public GetAllWorkspacesHandler(IWorkspaceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WorkspaceDTO>> Handle(GetAllWorkspacesQuery request, CancellationToken cancellationToken)
    {
        var workspaces = await _repository.GetAllAsync(cancellationToken);

        if (!workspaces.Any())
        {
            return Enumerable.Empty<WorkspaceDTO>();
        }

        return _mapper.Map<IEnumerable<WorkspaceDTO>>(workspaces);
    }
}
