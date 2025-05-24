using CoWorking.Application.Interfaces.Repositories;
using CoWorking.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace CoWorking.Infrastructure.Persistence.Repositories;

internal class BookingRepository(CoWorkingDbContext dbContext) : IBookingRepository
{
    public async Task CreateAsync(Booking entity, CancellationToken cancellationToken)
    {
        await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
    public async Task<IEnumerable<Booking>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Bookings
            .Include(b => b.Room)
                .ThenInclude(r => r.Workspace)
                    .ThenInclude(w => w.Pictures)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var booking = await dbContext.Bookings
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        dbContext.Bookings.Remove(booking!);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext.Bookings
        .AnyAsync(b => b.Id == id, cancellationToken);
    }
}
