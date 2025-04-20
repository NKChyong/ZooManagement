// AnimalDto.cs
namespace ZooManagement.Application.DTOs;
public record AnimalDto(Guid Id, string Species, string Name,
    DateTime DateOfBirth, string Gender,
    string FavouriteFood, string Status, Guid? EnclosureId);