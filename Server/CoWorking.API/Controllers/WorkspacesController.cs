using CoWorking.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoWorking.API.Controllers;

[ApiController]
[Route("api/workspaces")]
public class WorkspacesController : ControllerBase
{
    private readonly IWorkspaceRepository _workspaceRepository;
    public WorkspacesController(IWorkspaceRepository workspaceRepository)
    {
        _workspaceRepository = workspaceRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var workspaces = await _workspaceRepository.GetAllAsync();

            if (!workspaces.Any())
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
}
