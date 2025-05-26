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

    public async Task<Booking?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await dbContext.Bookings
            .Include(b => b.Room)
                .ThenInclude(r => r.Workspace)
                    .ThenInclude(w => w.Rooms)
            .AsSplitQuery()
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
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
    public async Task UpdateAsync(Booking entity, CancellationToken cancellationToken)
    {
        dbContext.Bookings.Update(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var booking = await dbContext.Bookings
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        dbContext.Bookings.Remove(booking!);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Workspace>> GetWorkspacesInfoAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Workspaces
            .Include(w => w.Rooms)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> RoomExistsByIdAsync(int roomId, CancellationToken cancellationToken)
    {
        return await dbContext.Rooms.AnyAsync(r => r.Id == roomId, cancellationToken);
    }

    public async Task<bool> IsRoomAvailableAsync(int roomId, CancellationToken cancellationToken)
    {
        var quantity = await dbContext.Rooms
            .Where(r => r.Id == roomId)
            .Select(r => r.Quantity)
            .FirstOrDefaultAsync(cancellationToken);

        var bookings = await dbContext.Bookings
            .Where(b => b.RoomId == roomId).CountAsync(cancellationToken);
        
        return quantity > bookings;
    }

    public async Task<bool> IsOverlappingAsync(int roomId, DateTime start, DateTime end, CancellationToken cancellationToken)
    {
        return await dbContext.Bookings
            .AnyAsync(b => b.RoomId == roomId &&
                b.StartDateTime < end &&
                b.EndDateTime > start,
                cancellationToken);
    }

    public async Task<bool> IsOverlappingAsync(int roomId, int bookingId, DateTime start, DateTime end, CancellationToken cancellationToken)
    {
        return await dbContext.Bookings
            .AnyAsync(b => b.Id != bookingId &&
                b.RoomId == roomId &&
                b.StartDateTime < end &&
                b.EndDateTime > start,
                cancellationToken);
    }
}
