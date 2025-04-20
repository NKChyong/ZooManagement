// FeedingSchedulesController.cs
using Microsoft.AspNetCore.Mvc;
using ZooManagement.Application.Interfaces;
using ZooManagement.Application.Services;
using ZooManagement.Domain.ValueObjects;

namespace ZooManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedingSchedulesController : ControllerBase
{
    private readonly IFeedingScheduleRepository _schedules;
    private readonly FeedingOrganizationService _feeding;

    public FeedingSchedulesController(IFeedingScheduleRepository schedules,
        FeedingOrganizationService feeding)
    {
        _schedules = schedules;
        _feeding = feeding;
    }

    [HttpGet]
    public async Task<ActionResult> Get() => Ok(await _schedules.GetAllAsync());

    [HttpPost]
    public async Task<ActionResult> Create(CreateFeedingRequest req)
    {
        await _feeding.AddFeedingAsync(req.AnimalId, req.TimeUtc, new Food(req.Food));
        return Ok();
    }

    [HttpPut("{id:guid}/execute")]
    public async Task<ActionResult> Execute(Guid id)
    {
        await _feeding.ExecuteFeedingAsync(id);
        return NoContent();
    }
}
public record CreateFeedingRequest(Guid AnimalId, DateTime TimeUtc, string Food);