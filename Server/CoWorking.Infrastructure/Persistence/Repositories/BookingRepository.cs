using CoWorking.Application.Interfaces.Repositories;
using CoWorking.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoWorking.Infrastructure.Persistence.Repositories;

internal class BookingRepository(CoWorkingDbContext dbContext) : IBookingRepository
{
    public async Task<IEnumerable<Booking>> GetAllAsync()
    {
        return await dbContext.Bookings
            .Include(b => b.Room)
                .ThenInclude(r => r.Workspace)
                    .ThenInclude(w => w.Pictures)
            .ToListAsync();
    }
}
