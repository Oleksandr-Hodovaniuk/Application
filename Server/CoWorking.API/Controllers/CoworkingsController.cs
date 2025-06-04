using AutoMapper;
using CoWorking.Application.DTOs.Coworking;
using CoWorking.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoWorking.API.Controllers;

[ApiController]
[Route("api/coworkings")]
public class CoworkingsController : ControllerBase
{
    private readonly ICoworkingRepository _repository;
	private readonly IMapper _mapper;
	public CoworkingsController(ICoworkingRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	[HttpGet]
	public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
	{
		var coworkings = await _repository.GetAllAsync(cancellationToken);

		return Ok(_mapper.Map<IEnumerable<CoworkingDTO>>(coworkings));
	}
}
