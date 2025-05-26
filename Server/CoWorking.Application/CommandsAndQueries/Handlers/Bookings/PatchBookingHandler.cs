using AutoMapper;
using CoWorking.Application.CommandsAndQueries.Commands.Bookings;
using CoWorking.Application.Exceptions;
using CoWorking.Application.Interfaces.Repositories;
using CoWorking.Core.Entities;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Handlers.Bookings;

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

        // Triggers only if the new room is different from the current one.
        if (booking.RoomId != request.dto.SelectedRoomId)
        {
            // Triggers only if the room with the given id doesn't exist.
            if (!await _repository.RoomExistsByIdAsync(request.dto.SelectedRoomId!.Value, cancellationToken))
            {
                throw new NotFoundException("Room with given id doesn't exist.");
            }

            // Triggers only if there is no available room with the given id.
            if (!await _repository.IsRoomAvailableAsync(request.dto.SelectedRoomId!.Value, cancellationToken))
            {
                // Triggers only if the given booking time overlaps.
                if (await _repository.IsOverlappingAsync(request.dto.SelectedRoomId!.Value,                   
                    request.dto.StartDateTime!.Value,
                    request.dto.EndDateTime!.Value,
                    cancellationToken))
                {
                    throw new BusinessException("Selected time is not available.");
                }

                _mapper.Map(request.dto, booking);

                await _repository.UpdateAsync(booking, cancellationToken);

                return;
            }

            _mapper.Map(request.dto, booking);

            await _repository.UpdateAsync(booking, cancellationToken);

            return;
        }

        // Triggers only if the room is the same.
        if (await _repository.IsOverlappingAsync(request.dto.SelectedRoomId!.Value,
            booking.Id,
            request.dto.StartDateTime!.Value,
            request.dto.EndDateTime!.Value,
            cancellationToken))
        {
            throw new BusinessException("Selected time is not available.");
        }

        _mapper.Map(request.dto, booking);

        await _repository.UpdateAsync(booking, cancellationToken);

        return;
    }
}
