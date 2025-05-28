using CoWorking.Application.DTOs.Booking;
using CoWorking.Application.DTOs.Room;
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
    /// Determines whether the room with the given ID exists.
    /// </summary>
    Task<bool> RoomExistsByIdAsync(int roomId, CancellationToken cancellationToken);
    /// <summary>
    /// Determines whether a room is available for booking within a specified time range (for create).
    /// </summary>
    Task<bool> RoomAvailableAsync(RoomAvailableDTO dto, CancellationToken cancellationToken);
    /// <summary>
    /// Determines whether a room is available for booking within a specified time range,
    /// excluding the specified existing booking (for patch).
    /// </summary>
    Task<bool> RoomAvailableAsync(int roomId, int bookingId, DateTime start, DateTime end, CancellationToken cancellationToken);
    /// <summary>
    /// Determines whether any booking for the specified email overlaps with the given time range (for craete).
    /// </summary>
    Task<bool> IsBookingOverlappingAsync(BookingOverlappingDTO dto, CancellationToken cancellationToken);
    /// <summary>
    /// Determines whether any booking for the specified email, excluding the given booking ID,
    /// overlaps with the given time range (for patch).
    /// </summary>
    Task<bool> IsBookingOverlappingAsync(string email, int bookingId, DateTime start, DateTime end, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes all expired bookings.
    /// </summary>
    Task DeleteExpiredBookingsAsync(CancellationToken cancellationToken);
}
