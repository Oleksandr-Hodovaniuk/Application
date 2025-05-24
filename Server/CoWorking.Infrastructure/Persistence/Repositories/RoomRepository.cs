using CoWorking.Application.Interfaces.Repositories;
using CoWorking.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoWorking.Infrastructure.Persistence.Repositories;

internal class RoomRepository(CoWorkingDbContext dbContext) : IRoomRepository
{
    public async Task<IEnumerable<Room>> GetRoomsByWorkspaceTypeAsync(string workspaceType, CancellationToken cancellationToken)
    {
        return await dbContext.Rooms
            .Include(r => r.Workspace)
            .Where(r => r.Workspace.Type.ToString() == workspaceType)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
    }
}
