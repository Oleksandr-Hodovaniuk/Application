using AutoMapper;
using CoWorking.Application.CommandsAndQueries.Bookings.Queries;
using CoWorking.Application.CommandsAndQueries.Coworkings.Queries;
using CoWorking.Application.DTOs.Coworking;
using CoWorking.Application.Interfaces.Repositories;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Coworkings.Handlers;

public class GetAllCoworkingsHandler : IRequestHandler<GetAllCoworkingsQuery, IEnumerable<CoworkingDTO>>
{
    private readonly ICoworkingRepository _repository;
    private readonly IMapper _mapper;
    public GetAllCoworkingsHandler(ICoworkingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CoworkingDTO>> Handle(GetAllCoworkingsQuery request, CancellationToken cancellationToken)
    {
        var coworkings = await _repository.GetAllAsync(cancellationToken);

        if (!coworkings.Any())
        {
            return Enumerable.Empty<CoworkingDTO>();
        }

        return _mapper.Map<IEnumerable<CoworkingDTO>>(coworkings);
    }
}
