using CoWorking.Application.DTOs.AiAssistant;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.AiAssistant.Queries;

public record GetAnswerQuery(string question) : IRequest<AiAssistantResponseDTO>;

