using AutoMapper;
using CoWorking.Application.CommandsAndQueries.Commands.Bookings;
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
        var booking = _mapper.Map<Booking>(request.dto);

        await _repository.CreateAsync(booking, cancellationToken);
    }
}
