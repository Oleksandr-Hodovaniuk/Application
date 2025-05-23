using AutoMapper;
using CoWorking.Application.DTOs;
using CoWorking.Application.Interfaces.Repositories;
using CoWorking.Application.Workspaces.Queries;
using MediatR;

namespace CoWorking.Application.Workspaces.Hadlers;

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
