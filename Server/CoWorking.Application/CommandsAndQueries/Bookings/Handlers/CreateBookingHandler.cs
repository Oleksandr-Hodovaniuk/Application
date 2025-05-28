using AutoMapper;
using CoWorking.Application.CommandsAndQueries.Bookings.Commands;
using CoWorking.Application.DTOs.Booking;
using CoWorking.Application.DTOs.Room;
using CoWorking.Application.Exceptions;
using CoWorking.Application.Interfaces.Repositories;
using CoWorking.Core.Entities;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Bookings.Handlers;

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
        _ = await _repository.RoomExistsByIdAsync(request.dto.RoomId, cancellationToken)
            ? true
            : throw new NotFoundException("Room with given id doesn't exist.");

        var overlapDto = new BookingOverlappingDTO
        {
            Email = request.dto.Email,
            StartDateTime = request.dto.StartDateTime,
            EndDateTime = request.dto.EndDateTime,
        };

        _ = await _repository.IsBookingOverlappingAsync(overlapDto, cancellationToken)
            ? throw new BusinessException($"{request.dto.Email} already have a booking that overlaps with this time.")
            : false;

        var availableDto = new RoomAvailableDTO
        {
            RoomId = request.dto.RoomId,
            StartDateTime = request.dto.StartDateTime,
            EndDateTime = request.dto.EndDateTime
        };

        _ = await _repository.RoomAvailableAsync(availableDto, cancellationToken)
            ? true
            : throw new BusinessException("Unfortunately, there are no available rooms at this time."); ;    

        var booking = _mapper.Map<Booking>(request.dto);

        await _repository.CreateAsync(booking, cancellationToken);
    }
}
