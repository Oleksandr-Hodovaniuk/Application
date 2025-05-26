using CoWorking.Application.DTOs.Room;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Rooms.Queries;

public record GetRoomsByWorkspaceTypeQuery(string workspaceType) : IRequest<IEnumerable<RoomDTO>>;
