using AutoMapper;
using CoWorking.Application.CommandsAndQueries.Queries.Bookings;
using CoWorking.Application.DTOs;
using CoWorking.Application.Exceptions;
using CoWorking.Application.Interfaces.Repositories;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Handlers.Bookings;

public class GetByIdBookingHandler : IRequestHandler<GetByIdBookingQuery, BookingDTO>
{
    private readonly IBookingRepository _repository;
    private readonly IMapper _mapper;
    public GetByIdBookingHandler(IBookingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BookingDTO> Handle(GetByIdBookingQuery request, CancellationToken cancellationToken)
    {
        var booking = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (booking == null)
        {
            throw new NotFoundException($"Booking with id: {request.id} doesn't exist.");
        }
        
        var bookingDto = _mapper.Map<BookingDTO>(booking);

        return bookingDto;
    }
}
