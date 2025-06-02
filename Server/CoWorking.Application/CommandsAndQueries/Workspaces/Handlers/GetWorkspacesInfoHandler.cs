using AutoMapper;
using CoWorking.Application.CommandsAndQueries.Workspaces.Queries;
using CoWorking.Application.DTOs.Room;
using CoWorking.Application.DTOs.Workspace;
using CoWorking.Application.Interfaces.Repositories;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Workspaces.Handlers;

public class GetWorkspacesInfoHandler : IRequestHandler<GetWorkspacesInfoQuery, IEnumerable<DropDownWorkspaceDTO>>
{
    private readonly IWorkspaceRepository _repository;
    private readonly IMapper _mapper;
    public GetWorkspacesInfoHandler(IWorkspaceRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DropDownWorkspaceDTO>> Handle(GetWorkspacesInfoQuery request, CancellationToken cancellationToken)
    {
        var workspaces = await _repository.GetWorkspacesInfoAsync(cancellationToken);

        if (!workspaces.Any())
        {
            return new List<DropDownWorkspaceDTO>();
        }

        return _mapper.Map<List<DropDownWorkspaceDTO>>(workspaces);
    }
}
