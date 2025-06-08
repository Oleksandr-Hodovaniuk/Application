using CoWorking.Application.DTOs.Booking;
using CoWorking.Application.DTOs.Room;
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

    public async Task<IEnumerable<Booking>> GetAllForAiAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Bookings
            .Include(b => b.Room)
                .ThenInclude(r => r.Workspace)
                    .ThenInclude(w => w.Coworking)
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

    public async Task<bool> RoomAvailableAsync(RoomAvailableCreateDTO dto, CancellationToken cancellationToken)
    {
        var quantity = await dbContext.Rooms
            .Where(r => r.Id == dto.RoomId)
            .Select(r => r.Quantity)
            .FirstOrDefaultAsync(cancellationToken);

        if (quantity == 0)
        {
            return false;
        }

        var overlappingCount = await dbContext.Bookings
            .Where(b => b.RoomId == dto.RoomId &&
                b.StartDateTime < dto.EndDateTime &&
                b.EndDateTime > dto.StartDateTime)
            .CountAsync(cancellationToken);

        return quantity > overlappingCount;
    }

    public async Task<bool> RoomAvailableAsync(RoomAvailabePatchDTO dto, CancellationToken cancellationToken)
    {
        var quantity = await dbContext.Rooms
            .Where(r => r.Id == dto.RoomId)
            .Select(r => r.Quantity)
            .FirstOrDefaultAsync(cancellationToken);

        if (quantity == 0)
            return false;

        var overlappingCount = await dbContext.Bookings
            .Where(b => b.RoomId == dto.RoomId &&
                        b.Id != dto.BookingId &&
                        b.StartDateTime < dto.EndDateTime &&
                        b.EndDateTime > dto.StartDateTime)
            .CountAsync(cancellationToken);

        return quantity > overlappingCount;
    }

    public async Task<bool> IsBookingOverlappingAsync(BookingOverlappingCreateDTO dto, CancellationToken cancellationToken)
    {
        return await dbContext.Bookings
                .AnyAsync(b => b.Email == dto.Email &&
                b.StartDateTime < dto.EndDateTime &&
                b.EndDateTime > dto.StartDateTime,
                cancellationToken);
    }

    public async Task<bool> IsBookingOverlappingAsync(BookingOverlappingPatchDTO dto, CancellationToken cancellationToken)
    {
        return await dbContext.Bookings
                .AnyAsync(b => b.Id != dto.BookingId &&
                b.Email == dto.Email &&
                b.StartDateTime < dto.EndDateTime &&
                b.EndDateTime > dto.StartDateTime,
                cancellationToken);
    }

    public async Task DeleteExpiredBookingsAsync(CancellationToken cancellationToken)
    {
        var now = DateTime.Now;

        var bookings =  await dbContext.Bookings
                        .Where(b => b.EndDateTime <= now)
                        .ToListAsync(cancellationToken);

        if (bookings.Any())
        {
            dbContext.Bookings.RemoveRange(bookings);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
