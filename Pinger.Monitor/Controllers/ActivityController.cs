using Microsoft.AspNetCore.Mvc;
using pinger.console.Models;
using Pinger.Monitor.Services;

namespace Pinger.Monitor.Controllers;

[Route("api/[controller]")]
public class ActivityController : ControllerBase
{
    private readonly IActivityService _activityService;

    public ActivityController(IActivityService activityService)
    {
        ArgumentNullException.ThrowIfNull(nameof(activityService));
        
        _activityService = activityService;
    }

    [HttpPost]
    public IActionResult Store([FromBody] HostPingResult? pingResult)
    {
        if (pingResult == null)
        {
            return BadRequest(nameof(pingResult));
        }

        try
        {
            _activityService.Store(pingResult);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
    
    [HttpGet]
    public IActionResult GetActivity()
    {
        try
        {
            return Ok(_activityService.GetData());
        }
        catch (Exception exception)
        {
            return Problem(exception.Message);
        }
    }
}