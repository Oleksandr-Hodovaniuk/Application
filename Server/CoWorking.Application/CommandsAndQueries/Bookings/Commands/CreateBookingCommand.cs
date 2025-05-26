using CoWorking.Application.DTOs.Booking;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Bookings.Commands;

public record CreateBookingCommand(CreateBookingDTO dto) : IRequest;
