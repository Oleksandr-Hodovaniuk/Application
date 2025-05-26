using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Bookings.Commands;

public record DeleteBookingCommand(int id) : IRequest;
