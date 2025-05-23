using AutoMapper;
using CoWorking.Application.DTOs;
using CoWorking.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace CoWorking.API.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingsController : ControllerBase
{
    private readonly IBookingRepository _bookingsRepository;
	public readonly IMapper _mapper;

    public BookingsController(IBookingRepository bookingsRepository, IMapper mapper)
	{
		_bookingsRepository = bookingsRepository;
		_mapper = mapper;
	}

	[HttpGet]
	public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
	{
		try
		{
			var bookings = await _bookingsRepository.GetAllAsync(cancellationToken);

			if (!bookings.Any())
			{
				return NoContent();
			}

			return Ok();
		}
		catch (Exception) 
		{
            return StatusCode(500, "Internal server error.");
        }
	}

	[HttpDelete("id")]
	public async Task<IActionResult> DeleteAsync(int id)
	{
        try
        {
			await _bookingsRepository.DeleteAsync(id);

            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error.");
        }
    }
}
