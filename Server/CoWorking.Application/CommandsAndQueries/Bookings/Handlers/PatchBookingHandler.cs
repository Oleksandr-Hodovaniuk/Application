using AutoMapper;
using CoWorking.Application.CommandsAndQueries.Bookings.Commands;
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
        var booking = await _repository.GetByIdAsync(request.id, cancellationToken);
        
        if (booking == null)
        {
            throw new NotFoundException("Booking with given id doesn't exist.");
        }

        var newRoomId = request.dto.SelectedRoomId ?? booking.RoomId;
        var newEmail = request.dto.Email ?? booking.Email;
        var newStart = request.dto.StartDateTime ?? booking.StartDateTime;
        var newEnd = request.dto.EndDateTime ?? booking.EndDateTime;

        if (await _repository.IsBookingOverlappingAsync(newEmail, booking.Id, newStart, newEnd, cancellationToken))
        {
            throw new BusinessException($"{newEmail} already have a booking that overlaps with this time.");
        }

        if (!await _repository.RoomAvailableAsync(newRoomId,
            booking.Id,
            newStart,
            newEnd,
            cancellationToken))
        {
            throw new BusinessException("Unfortunately, there are no available rooms at this time.");
        }

        _mapper.Map(request.dto, booking);

        await _repository.UpdateAsync(booking, cancellationToken);
    }
}
