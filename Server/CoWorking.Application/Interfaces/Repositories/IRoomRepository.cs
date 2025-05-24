using CoWorking.Core.Entities;

namespace CoWorking.Application.Interfaces.Repositories;

public interface IRoomRepository  
{
    Task<IEnumerable<Room>> GetRoomsByWorkspaceTypeAsync(string workspaceType, CancellationToken cancellationToken);
}
