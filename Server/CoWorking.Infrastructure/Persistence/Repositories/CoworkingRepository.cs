using CoWorking.Application.Interfaces.Repositories;
using CoWorking.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoWorking.Infrastructure.Persistence.Repositories;

internal class CoworkingRepository(CoWorkingDbContext dbContext) : ICoworkingRepository
{
    public async Task<IEnumerable<Coworking>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Coworkings
            .Include(c => c.Workspaces)
                .ThenInclude(w => w.Rooms)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Workspace>> GetAllWorkspacesByCoworkingIdAsync(int coworkingId, CancellationToken cancellationToken)
    {
        return await dbContext.Workspaces
            .Include(w => w.Pictures)
            .Include(w => w.Rooms)
                .ThenInclude(r => r.Bookings)
            .Include(w => w.WorkspaceIcons)
                .ThenInclude(wi => wi.Icon)
            .Where(w => w.CoworkingId == coworkingId)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
    }
}
