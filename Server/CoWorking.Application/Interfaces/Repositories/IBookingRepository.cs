using CoWorking.Core.Entities;
using System.Threading;

namespace CoWorking.Application.Interfaces.Repositories;

public interface IBookingRepository : IGenericRepository<Booking>
{
    /// <summary>
    /// Creates the booking with the given entity.
    /// </summary>
    Task CreateAsync(Booking entity, CancellationToken cancellationToken);
    /// <summary>
    /// Returns the booking with the given ID.
    /// </summary>
    Task<Booking?> GetByIdAsync(int id, CancellationToken cancellationToken);
    /// <summary>
    /// Updates the booking with the given entity.
    /// </summary>
    Task UpdateAsync(Booking entity, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes the booking with the given ID.
    /// </summary>
    Task DeleteAsync(int id, CancellationToken cancellationToken);
    /// <summary>
    /// Checks whether the room with the given ID exists.
    /// </summary>
    Task<bool> RoomExistsByIdAsync(int roomId, CancellationToken cancellationToken);
    /// <summary>
    /// Checks whether any booking for the specified room overlaps with the given time range.
    /// </summary>
    Task<bool> IsOverlappingAsync(int roomId, DateTime start, DateTime end, CancellationToken cancellationToken);
    /// <summary>
    /// Checks whether any booking for the specified room, excluding the given booking ID, overlaps with the given time range.
    /// </summary>
    Task<bool> IsOverlappingAsync(int roomId, int bookingId, DateTime start, DateTime end, CancellationToken cancellationToken);
    /// <summary>
    /// Checks whether the room with the given ID is currently available for booking.
    /// </summary>
    Task<bool> IsRoomAvailableAsync(int roomId, CancellationToken cancellationToken);
}
