using AutoMapper;
using CoWorking.Application.CommandsAndQueries.Bookings.Commands;
using CoWorking.Application.DTOs.Booking;
using CoWorking.Application.DTOs.Room;
using CoWorking.Application.Exceptions;
using CoWorking.Application.Interfaces.Repositories;
using CoWorking.Core.Entities;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Bookings.Handlers;

public class PatchBookingHandler : IRequestHandler<PatchBookingCommand>
{
    private readonly IBookingRepository _repository;
    private readonly IMapper _mapper;
    public PatchBookingHandler(IBookingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(PatchBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _repository.GetByIdAsync(request.id, cancellationToken)
            ?? throw new NotFoundException("Booking with given id doesn't exist.");

        var overlapDto = new BookingOverlappingPatchDTO()
        {
            BookingId = booking.Id,
            Email = request.dto.Email ?? booking.Email,
            StartDateTime = request.dto.StartDateTime ?? booking.StartDateTime,
            EndDateTime = request.dto.EndDateTime ?? booking.EndDateTime
        };      

        _ = await _repository.IsBookingOverlappingAsync(overlapDto, cancellationToken)
            ? throw new BusinessException($"{overlapDto.Email} already have a booking that overlaps with this time.")
            : false;

        var availableDto = new RoomAvailabePatchDTO
        {
            BookingId = booking.Id,
            RoomId = request.dto.SelectedRoomId ?? booking.RoomId,
            StartDateTime = overlapDto.StartDateTime,
            EndDateTime = overlapDto.EndDateTime
        };

        _ = await _repository.RoomAvailableAsync(availableDto, cancellationToken)
            ? true 
            : throw new BusinessException("Unfortunately, there are no available rooms at this time.");

        _mapper.Map(request.dto, booking);

        await _repository.UpdateAsync(booking, cancellationToken);
    }
}
