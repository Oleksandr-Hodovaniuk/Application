using AutoMapper;
using CoWorking.Application.CommandsAndQueries.Queries.Rooms;
using CoWorking.Application.DTOs.Room;
using CoWorking.Application.Interfaces.Repositories;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Handlers.Rooms;

public class GetRoomsByWorkspaceTypeHandler : IRequestHandler<GetRoomsByWorkspaceTypeQuery, IEnumerable<RoomDTO>>
{
    private readonly IRoomRepository _repository;
    private readonly IMapper _mapper;
    public GetRoomsByWorkspaceTypeHandler(IRoomRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RoomDTO>> Handle(GetRoomsByWorkspaceTypeQuery request, CancellationToken cancellationToken)
    {
        var rooms = await _repository
            .GetRoomsByWorkspaceTypeAsync(request.workspaceType, cancellationToken);

        return _mapper.Map<IEnumerable<RoomDTO>>(rooms);
    }
}
