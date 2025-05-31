using AutoMapper;
using CoWorking.Application.CommandsAndQueries.Bookings.Commands;
using CoWorking.Application.CommandsAndQueries.Bookings.Queries;
using CoWorking.Application.DTOs.Booking;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;


namespace CoWorking.API.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<CreateBookingDTO> _createValidator;
    private readonly IValidator<PatchBookingDTO> _patchValidator;
    public BookingsController(IMediator mediator,
        IValidator<CreateBookingDTO> createValidator,
        IValidator<PatchBookingDTO> patchValidator)
    {
        _mediator = mediator;
        _createValidator = createValidator;
        _patchValidator = patchValidator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateBookingDTO dto, CancellationToken cancellationToken)
    {
        var validationResult = await _createValidator.ValidateAsync(dto, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

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

        var validationResult = await _patchValidator.ValidateAsync(bookingDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        await _mediator.Send(new PatchBookingCommand(id, bookingDto), cancellationToken);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
	{
        await _mediator.Send(new DeleteBookingCommand(id), cancellationToken);

        return NoContent();
    }
}
