using CoWorking.Application.DTOs.Room;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Queries.Rooms;

public record GetRoomsByWorkspaceTypeQuery(string workspaceType) : IRequest<IEnumerable<RoomDTO>>;
