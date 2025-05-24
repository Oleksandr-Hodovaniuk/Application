using CoWorking.Application.DTOs;
using MediatR;


namespace CoWorking.Application.CommandsAndQueries.Commands.Bookings;

public record CreateBookingCommand(BookingCreateDTO dto) : IRequest;
