using CoWorking.Application.DTOs.Booking;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Bookings.Queries;

public record GetByIdBookingQuery(int id) : IRequest<PatchBookingDTO>;
