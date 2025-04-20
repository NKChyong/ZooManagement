// EnclosuresController.cs
using Microsoft.AspNetCore.Mvc;
using ZooManagement.Application.Interfaces;
using ZooManagement.Domain.Entities;
using ZooManagement.Domain.ValueObjects;

namespace ZooManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnclosuresController : ControllerBase
{
    private readonly IEnclosureRepository _enclosures;

    public EnclosuresController(IEnclosureRepository enclosures) => _enclosures = enclosures;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Enclosure>>> GetAll()
        => Ok(await _enclosures.GetAllAsync());

    [HttpPost]
    public async Task<ActionResult> Create(CreateEnclosureRequest req)
    {
        var enc = new Enclosure(Guid.NewGuid(), new EnclosureType(req.Type),
            req.SizeSqM, req.MaxCapacity);
        await _enclosures.AddAsync(enc);
        await _enclosures.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAll), null);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _enclosures.RemoveAsync(id);
        await _enclosures.SaveChangesAsync();
        return NoContent();
    }
}
public record CreateEnclosureRequest(string Type, double SizeSqM, int MaxCapacity);