using AutoMapper;
using CoWorking.Application.CommandsAndQueries.Bookings.Queries;
using CoWorking.Application.DTOs.Booking;
using CoWorking.Application.Interfaces.Repositories;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Bookings.Handlers;

public class GetAllBookingsHandler : IRequestHandler<GetAllBookingsQuery, IEnumerable<BookingDTO>>
{
    private readonly IBookingRepository _repository;
    private readonly IMapper _mapper;
    public GetAllBookingsHandler(IBookingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookingDTO>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
    {
        var bookings = await _repository.GetAllAsync(cancellationToken);

        if (!bookings.Any())
        {
            return Enumerable.Empty<BookingDTO>();
        }

        return _mapper.Map<IEnumerable<BookingDTO>>(bookings);
    }
}
