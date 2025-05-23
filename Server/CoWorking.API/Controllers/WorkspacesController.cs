using CoWorking.Application.CommandsAndQueries.Queries.Workspaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoWorking.API.Controllers;

[ApiController]
[Route("api/workspaces")]
public class WorkspacesController : ControllerBase
{
    private readonly IMediator _mediator;
    public WorkspacesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var workspaces = await _mediator.Send(new GetAllWorkspacesQuery(), cancellationToken);

        if (!workspaces.Any())
        {
            return NoContent();
        }

        return Ok(workspaces);
    }
}
