using CoWorking.Core.Entities;
using System.Threading;

namespace CoWorking.Application.Interfaces.Repositories;

public interface IBookingRepository : IGenericRepository<Booking>
{
    Task CreateAsync(Booking entity, CancellationToken cancellationToken);
    Task<Booking?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task UpdateAsync(Booking entity, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
    Task<bool> RoomExistsByIdAsync(int roomId, CancellationToken cancellationToken);
    /// <summary>
    /// Checks if any booking for the specified room overlaps with the given time range.
    /// </summary>
    Task<bool> IsOverlappingAsync(int roomId, DateTime start, DateTime end, CancellationToken cancellationToken);
    /// <summary>
    /// Checks if any booking for the specified room, excluding the given booking ID, overlaps with the given time range.
    /// </summary>
    Task<bool> IsOverlappingAsync(int roomId, int bookingId, DateTime start, DateTime end, CancellationToken cancellationToken);
    Task<bool> IsAvailableAsync(int roomId, CancellationToken cancellationToken);
}
