using AutoMapper;
using CoWorking.Application.CommandsAndQueries.AiAssistant.Queries;
using CoWorking.Application.DTOs.AiAssistant;
using CoWorking.Application.Interfaces.Repositories;
using CoWorking.Application.Interfaces.Services;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.AiAssistant.Handlers;

public class GetAnswerHandler : IRequestHandler<GetAnswerQuery, string>
{
    private readonly IBookingRepository _repository;
    private readonly IAiAssistantService _service;
    private readonly IMapper _mapper;
    public GetAnswerHandler(IBookingRepository repository, IAiAssistantService service, IMapper mapper)
    {
        _repository = repository;
        _service = service;
        _mapper = mapper;
    }

    public async Task<string> Handle(GetAnswerQuery request, CancellationToken cancellationToken)
    {
        var bookings = await _repository.GetAllForAiAsync(cancellationToken);

        var bookingDtos = _mapper.Map<IEnumerable<AiBookingDTO>>(bookings);

        return await _service.AskAsync(request.question, bookingDtos, cancellationToken);
    }
}
