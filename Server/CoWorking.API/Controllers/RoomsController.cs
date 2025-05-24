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
    public async Task<IActionResult> GetRoomsByWorkspaceType(string workspaceType)
	{
		throw new NotImplementedException();
	}
}
