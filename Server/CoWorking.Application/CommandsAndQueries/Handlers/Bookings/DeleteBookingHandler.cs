using AutoMapper;
using CoWorking.Application.CommandsAndQueries.Commands.Bookings;
using CoWorking.Application.Exceptions;
using CoWorking.Application.Interfaces.Repositories;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Handlers.Bookings;

public class DeleteBookingHandler : IRequestHandler<DeleteBookingCommand>
{
    private readonly IBookingRepository _repository;
    private readonly IMapper _mapper;
    public DeleteBookingHandler(IBookingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
    {
        var booking = await _repository.GetByIdAsync(request.id, cancellationToken);

        if (booking == null)
        {
            throw new NotFoundException("Booking with given id does not exist.");
        }

        await _repository.DeleteAsync(request.id, cancellationToken);
    }
}
