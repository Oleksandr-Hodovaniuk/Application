using CoWorking.Core.Entities;

namespace CoWorking.Application.Interfaces.Repositories;

public interface ICoworkingRepository : IGenericRepository<Coworking>
{
    /// <summary>
    /// Returns all workspaces of coworking with given ID.
    /// </summary>
    Task<IEnumerable<Workspace>> GetAllWorkspacesByCoworkingIdAsync(int coworkingId, CancellationToken cancellationToken);
}
