using CoWorking.Application.CommandsAndQueries.Queries.Rooms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoWorking.API.Controllers;

[ApiController]
[Route("api/rooms")]
public class RoomsController : ControllerBase
{
	private readonly IMediator _mediator;
	public RoomsController(IMediator mediator)
	{
		_mediator = mediator;
	}

    [HttpGet("type/{workspaceType}")]
    public async Task<IActionResult> GetRoomsByWorkspaceType(string workspaceType, CancellationToken cancellationToken)
	{
		var rooms = await _mediator.Send(new GetRoomsByWorkspaceTypeQuery(workspaceType), cancellationToken);

		return Ok(rooms);
	}
}
