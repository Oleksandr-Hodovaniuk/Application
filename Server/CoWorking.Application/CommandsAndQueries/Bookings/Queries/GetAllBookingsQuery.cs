using CoWorking.Application.DTOs.Booking;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Bookings.Queries;

public record GetAllBookingsQuery :IRequest<IEnumerable<BookingDTO>>;
