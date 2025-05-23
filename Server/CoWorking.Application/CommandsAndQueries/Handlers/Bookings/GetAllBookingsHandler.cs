using AutoMapper;
using CoWorking.Application.CommandsAndQueries.Queries.Bookings;
using CoWorking.Application.DTOs;
using CoWorking.Application.Interfaces.Repositories;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Hendlers.Bookings;

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
