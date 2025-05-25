using AutoMapper;
using CoWorking.Application.CommandsAndQueries.Commands.Bookings;
using CoWorking.Application.CommandsAndQueries.Queries.Bookings;
using CoWorking.Application.DTOs.Booking;
using CoWorking.Application.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
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
    public async Task<IActionResult> CreateAsync([FromBody] CreateBookingDTO dto, CancellationToken cancellationToken)
    {
        await _mediator.Send(new CreateBookingCommand(dto));

        return Created();
    }

    [HttpGet("id")]
    public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var booking = await _mediator.Send(new GetByIdBookingQuery(id), cancellationToken);

        return Ok(booking);
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

    [HttpPatch("id")]
    public async Task<IActionResult> PatchAsync(int id,
        [FromBody] JsonPatchDocument<PatchBookingDTO> patchDoc,
        CancellationToken cancellationToken)
    {
        if (patchDoc == null)
        {
            return BadRequest();
        }

        var bookingDto = await _mediator.Send(new GetByIdBookingQuery(id), cancellationToken);

        if (bookingDto == null)
        {
            return NotFound();
        }

        patchDoc.ApplyTo(bookingDto, ModelState);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _mediator.Send(new PatchBookingCommand(id, bookingDto), cancellationToken);
        
        return NoContent();
    }

	[HttpDelete("id")]
	public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
	{
        await _mediator.Send(new DeleteBookingCommand(id), cancellationToken);

        return NoContent();
    }
}
