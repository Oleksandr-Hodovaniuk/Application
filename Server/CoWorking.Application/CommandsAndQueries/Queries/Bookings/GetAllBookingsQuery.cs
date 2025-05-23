using CoWorking.Application.DTOs;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Queries.Bookings;

public record GetAllBookingsQuery :IRequest<IEnumerable<BookingDTO>>;
