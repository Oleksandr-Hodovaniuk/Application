using CoWorking.Core.Entities;

namespace CoWorking.Application.Interfaces.Repositories;

public interface IRoomRepository  
{
    /// <summary>
    /// Returns all rooms with the given workspace type.
    /// </summary>
    Task<IEnumerable<Room>> GetRoomsByWorkspaceTypeAsync(string workspaceType, CancellationToken cancellationToken);
}
