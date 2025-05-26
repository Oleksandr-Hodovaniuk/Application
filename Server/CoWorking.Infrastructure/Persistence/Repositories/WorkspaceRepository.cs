using CoWorking.Application.DTOs.Workspace;
using CoWorking.Application.Interfaces.Repositories;
using CoWorking.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoWorking.Infrastructure.Persistence.Repositories;

internal class WorkspaceRepository(CoWorkingDbContext dbContext) : IWorkspaceRepository
{
    public async Task<IEnumerable<Workspace>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Workspaces
            .Include(w => w.Pictures)
            .Include(w => w.Rooms)
                .ThenInclude(r => r.Bookings)
            .Include(w => w.WorkspaceIcons)
                .ThenInclude(wi => wi.Icon)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Workspace>> GetWorkspacesInfoAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Workspaces
            .Include(w => w.Rooms)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
    }
}
