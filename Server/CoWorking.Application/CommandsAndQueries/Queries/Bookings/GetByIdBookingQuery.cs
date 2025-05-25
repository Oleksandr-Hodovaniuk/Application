using CoWorking.Application.DTOs.Booking;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Queries.Bookings;

public record GetByIdBookingQuery(int id) : IRequest<PatchBookingDTO>;
