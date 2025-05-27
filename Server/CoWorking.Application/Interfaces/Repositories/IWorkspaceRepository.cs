using CoWorking.Core.Entities;

namespace CoWorking.Application.Interfaces.Repositories;

public interface IWorkspaceRepository : IGenericRepository<Workspace>
{
    /// <summary>
    /// Returns the maximum booking duration for a workspace by the given room ID.
    /// </summary>
    Task<int?> GetWorkspaceMaxDurationAsync(int roomId, CancellationToken cancellationToken);
}
