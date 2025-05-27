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
            throw new NotFoundException($"Booking with given id doesn't exist.");
        }

        var newRoomId = request.dto.SelectedRoomId ?? booking.RoomId;
        var newStart = request.dto.StartDateTime ?? booking.StartDateTime;
        var newEnd = request.dto.EndDateTime ?? booking.EndDateTime;

        // Triggers only if the new room is different from the current one.
        if (booking.RoomId != newRoomId)
        {
            // Triggers only if the room with the given id doesn't exist.
            if (!await _repository.RoomExistsByIdAsync(newRoomId, cancellationToken))
            {
                throw new NotFoundException("Room with given id doesn't exist.");
            }

            // Triggers only if the given booking time overlaps.
            if (await _repository.IsOverlappingAsync(newRoomId, newStart, newEnd, cancellationToken))
            {
                throw new BusinessException("Selected time is not available.");
            }
        }
        else
        {
            if (await _repository.IsOverlappingAsync(newRoomId, booking.Id, newStart, newEnd, cancellationToken))
            {
                throw new BusinessException("Selected time is not available.");
            }
        }

        _mapper.Map(request.dto, booking);

        await _repository.UpdateAsync(booking, cancellationToken);
    }
}
