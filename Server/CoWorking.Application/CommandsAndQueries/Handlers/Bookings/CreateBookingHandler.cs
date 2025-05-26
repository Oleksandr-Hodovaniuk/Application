using AutoMapper;
using CoWorking.Application.CommandsAndQueries.Commands.Bookings;
using CoWorking.Application.Exceptions;
using CoWorking.Application.Interfaces.Repositories;
using CoWorking.Core.Entities;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Handlers.Bookings;

public class CreateBookingHandler : IRequestHandler<CreateBookingCommand>
{
    private readonly IBookingRepository _repository;
    private readonly IMapper _mapper;
    public CreateBookingHandler(IBookingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        // Triggers only if the room with the given id doesn't exist.
        if (!await _repository.RoomExistsByIdAsync(request.dto.RoomId, cancellationToken))
        {
            throw new NotFoundException("Room with given id doesn't exist.");
        }

        if (!await _repository.IsRoomAvailableAsync(request.dto.RoomId, cancellationToken))
        {
            if (await _repository.IsOverlappingAsync(request.dto.RoomId,
                request.dto.StartDateTime,
                request.dto.EndDateTime,
                cancellationToken))
            {
                throw new BusinessException("Selected time is not available.");
            }

            var booking1 = _mapper.Map<Booking>(request.dto);

            await _repository.CreateAsync(booking1, cancellationToken);
            return;
        }

        var booking2 = _mapper.Map<Booking>(request.dto);

        await _repository.CreateAsync(booking2, cancellationToken);
    }
}
