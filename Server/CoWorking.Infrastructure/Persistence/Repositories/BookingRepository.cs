using CoWorking.Application.Interfaces.Repositories;
using CoWorking.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace CoWorking.Infrastructure.Persistence.Repositories;

internal class BookingRepository(CoWorkingDbContext dbContext) : IBookingRepository
{
    public async Task<IEnumerable<Booking>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Bookings
            .Include(b => b.Room)
                .ThenInclude(r => r.Workspace)
                    .ThenInclude(w => w.Pictures)
            .ToListAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var booking = await dbContext.Bookings.FindAsync(id);

        dbContext.Bookings.Remove(booking!);
        await dbContext.SaveChangesAsync();
    }
}
