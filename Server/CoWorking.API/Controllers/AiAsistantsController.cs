using CoWorking.Application.CommandsAndQueries.AiAssistant.Queries;
using CoWorking.Application.DTOs.AiAssistant;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoWorking.API.Controllers;
[ApiController]
[Route("api/aiasistants")]
public class AiAsistantsController : ControllerBase
{
    private readonly IMediator _mediator;
    public AiAsistantsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> GetAnswerAsync([FromBody] AiAssistantRequestDTO request, CancellationToken cancellationToken)
    {
        if(string.IsNullOrWhiteSpace(request.Question))
        {
            return BadRequest("Field cannot be empty or whitespace.");
        }

        var answer = await _mediator.Send(new GetAnswerQuery(request.Question), cancellationToken);

        return Ok(answer);
    }
}
