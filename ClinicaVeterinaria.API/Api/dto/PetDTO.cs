using ClinicaVeterinaria.API.Api.model;

namespace ClinicaVeterinaria.API.Api.dto;

internal class PetFindAllDTO
{
    public PetFindAllDTO(string photo, string name, string race, string species, Sex sex)
    {
        Photo = photo;
        Name = name;
        Race = race;
        Species = species;
        Sex = sex;
    }

    public string Photo { get; set; }
    public string Name { get; set; }
    public string Race { get; set; }
    public string Species { get; set; }
    public Sex Sex { get; set; }
}

internal class PetIdDTO
{
    public PetIdDTO(string photo, string name, string race, string species, Sex sex, DateOnly birthDate, double weight, double size, History history, UserDTOshort ownerDto)
    {
        Photo = photo;
        Name = name;
        Race = race;
        Species = species;
        Sex = sex;
        BirthDate = birthDate;
        Weight = weight;
        Size = size;
        History = history;
        OwnerDTO = ownerDto;
    }

    public string Photo { get; set; }
    public string Name { get; set; }
    public string Race { get; set; }
    public string Species { get; set; }
    public Sex Sex { get; set; }
    public DateOnly BirthDate { get; set; }
    public double Weight { get; set; }
    public double Size { get; set; }
    public History History { get; set; }
    public UserDTOshort OwnerDTO { get; set; }
}

internal class PetNoPhotoDTO
{
    private string Name { get; set; }
    private string Race { get; set; }
    private string Species { get; set; }
    private Sex Sex { get; set; }

    public PetNoPhotoDTO(string name, string race, string species, Sex sex)
    {
        Name = name;
        Race = race;
        Species = species;
        Sex = sex;
    }
}