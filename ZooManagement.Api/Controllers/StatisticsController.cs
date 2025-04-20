// StatisticsController.cs
using Microsoft.AspNetCore.Mvc;
using ZooManagement.Application.Services;

namespace ZooManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    private readonly ZooStatisticsService _stats;
    public StatisticsController(ZooStatisticsService stats) => _stats = stats;

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var (animals, enclosures, free) = await _stats.GetAsync();
        return Ok(new { animals, enclosures, freeEnclosures = free });
    }
}