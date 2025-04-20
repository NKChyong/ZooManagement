// AnimalsController.cs
using Microsoft.AspNetCore.Mvc;
using ZooManagement.Application.Interfaces;
using ZooManagement.Application.DTOs;
using ZooManagement.Domain.Entities;
using ZooManagement.Domain.ValueObjects;
using ZooManagement.Domain.Enums;
using ZooManagement.Application.Services;

namespace ZooManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalRepository _animals;
    private readonly AnimalTransferService _transfer;

    public AnimalsController(IAnimalRepository animals, AnimalTransferService transfer)
    {
        _animals = animals;
        _transfer = transfer;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AnimalDto>>> GetAll()
        => Ok((await _animals.GetAllAsync()).Select(ToDto));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AnimalDto>> Get(Guid id)
    {
        var animal = await _animals.GetAsync(id);
        if (animal is null) return NotFound();
        return Ok(ToDto(animal));
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateAnimalRequest req)
    {
        var animal = new Animal(Guid.NewGuid(),
                                new Species(req.Species),
                                new AnimalName(req.Name),
                                req.DateOfBirth,
                                Enum.Parse<Gender>(req.Gender, true),
                                new Food(req.FavouriteFood));
        await _animals.AddAsync(animal);
        await _animals.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = animal.Id }, ToDto(animal));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _animals.RemoveAsync(id);
        await _animals.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id:guid}/move/{enclosureId:guid}")]
    public async Task<ActionResult> Move(Guid id, Guid enclosureId)
    {
        await _transfer.MoveAsync(id, enclosureId);
        return NoContent();
    }

    private static AnimalDto ToDto(Animal a) =>
        new(a.Id, a.Species.Value, a.Name.Value, a.DateOfBirth,
            a.Gender.ToString(), a.FavouriteFood.Value, a.Status.ToString(), a.EnclosureId);
}

public record CreateAnimalRequest(string Species, string Name,
                                  DateTime DateOfBirth, string Gender, string FavouriteFood);
