using CoWorking.Application.DTOs.Coworking;
using MediatR;

namespace CoWorking.Application.CommandsAndQueries.Coworkings.Queries;

public record GetAllCoworkingsQuery : IRequest<IEnumerable<CoworkingDTO>>;
