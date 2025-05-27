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

        if (booking.RoomId != newRoomId)
        {
            if (!await _repository.RoomExistsByIdAsync(newRoomId, cancellationToken))
            {
                throw new NotFoundException("Room with given id doesn't exist.");
            }

            if (await _repository.IsBookingOverlappingAsync(newRoomId, booking.Id, newStart, newEnd, cancellationToken))
            {
                throw new BusinessException("Unfortunately, there are no available rooms at this time.");
            }
        }
        else
        {
            if (await _repository.IsBookingOverlappingAsync(newRoomId, booking.Id, newStart, newEnd, cancellationToken))
            {
                throw new BusinessException("Unfortunately, there are no available rooms at this time.");
            }
        }

        _mapper.Map(request.dto, booking);

        await _repository.UpdateAsync(booking, cancellationToken);
    }
}
