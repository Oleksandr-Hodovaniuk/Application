using AutoMapper;
using CoWorking.Application.CommandsAndQueries.Queries.Bookings;
using CoWorking.Application.DTOs.Booking;
using CoWorking.Application.Exceptions;
using CoWorking.Application.Interfaces.Repositories;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Handlers.Bookings;

public class GetByIdBookingHandler : IRequestHandler<GetByIdBookingQuery, PatchBookingDTO>
{
    private readonly IBookingRepository _repository;
    private readonly IMapper _mapper;
    public GetByIdBookingHandler(IBookingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PatchBookingDTO> Handle(GetByIdBookingQuery request, CancellationToken cancellationToken)
    {
        var booking = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (booking == null)
        {
            throw new NotFoundException($"Booking with given id doesn't exist.");
        }

        return _mapper.Map<PatchBookingDTO>(booking);
    }
}
