using AutoMapper;
using CoWorking.Application.CommandsAndQueries.Commands.Bookings;
using CoWorking.Application.CommandsAndQueries.Queries.Bookings;
using CoWorking.Application.DTOs;
using CoWorking.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace CoWorking.API.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingsController : ControllerBase
{
    private readonly IMediator _mediator;
    public BookingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] BookingCreateDTO dto, CancellationToken cancellationToken)
    {
        await _mediator.Send(new CreateBookingCommand(dto));

        return Created();
    }
    [HttpGet]
	public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
	{
        var bookings = await _mediator.Send(new GetAllBookingsQuery(), cancellationToken);

        if (!bookings.Any())
        {
            return NoContent();
        }
       
        return Ok(bookings);
    }

	[HttpDelete("id")]
	public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
	{
        await _mediator.Send(new DeleteBookingCommand(id), cancellationToken);

        return NoContent();
    }
}
