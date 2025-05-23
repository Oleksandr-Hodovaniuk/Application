using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Commands.Bookings;

public record DeleteBookingCommand(int id) : IRequest;
