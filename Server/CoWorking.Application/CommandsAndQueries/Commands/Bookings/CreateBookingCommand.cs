using CoWorking.Application.DTOs.Booking;
using MediatR;


namespace CoWorking.Application.CommandsAndQueries.Commands.Bookings;

public record CreateBookingCommand(CreateBookingDTO dto) : IRequest;
