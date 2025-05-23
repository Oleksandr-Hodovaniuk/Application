using CoWorking.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoWorking.API.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingsController : ControllerBase
{
    private readonly IBookingRepository _bookingsRepository;
	public BookingsController(IBookingRepository bookingsRepository)
	{
		_bookingsRepository = bookingsRepository;
	}

	[HttpGet]
	public async Task<IActionResult> GetAllAsync()
	{
		try
		{
			var bookings = await _bookingsRepository.GetAllAsync();

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
