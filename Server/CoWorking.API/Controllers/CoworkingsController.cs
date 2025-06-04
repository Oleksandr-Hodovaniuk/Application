using AutoMapper;
using CoWorking.Application.CommandsAndQueries.Coworkings.Queries;
using CoWorking.Application.DTOs.Coworking;
using CoWorking.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoWorking.API.Controllers;

[ApiController]
[Route("api/coworkings")]
public class CoworkingsController : ControllerBase
{
	private readonly IMediator _mediator;
	public CoworkingsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet]
	public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
	{
		var coworkings = await _mediator.Send(new GetAllCoworkingsQuery(), cancellationToken);

		if (!coworkings.Any())
		{
			return NoContent();
		}

		return Ok(coworkings);
	}
}
