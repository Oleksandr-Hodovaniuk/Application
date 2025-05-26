using AutoMapper;
using CoWorking.Application.CommandsAndQueries.Bookings.Queries;
using CoWorking.Application.DTOs.Booking;
using CoWorking.Application.DTOs.Workspace;
using CoWorking.Application.Exceptions;
using CoWorking.Application.Interfaces.Repositories;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Bookings.Handlers;

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

        var workspaces = await _repository.GetWorkspacesInfoAsync(cancellationToken);

        var workspacesDto = _mapper.Map<List<DropDownWorkspaceDTO>>(workspaces);

        var bookingDto = _mapper.Map<PatchBookingDTO>(booking);

        bookingDto.Workspaces = workspacesDto;

        return bookingDto;
    }
}
