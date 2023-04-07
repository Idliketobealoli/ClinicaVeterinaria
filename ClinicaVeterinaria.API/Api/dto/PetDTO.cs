using ClinicaVeterinaria.API.Api.model;

namespace ClinicaVeterinaria.API.Api.dto
{
    public class PetDTOshort
    {
        public PetDTOshort
            (
            Guid id, string photo, string name,
            string race, string species, Sex sex
            )
        {
            Id = id;
            Photo = photo;
            Name = name;
            Race = race;
            Species = species;
            Sex = sex;
        }

        public Guid Id { get; set; }
        public string Photo { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
        public string Species { get; set; }
        public Sex Sex { get; set; }
    }

    public class PetDTO
    {
        public PetDTO
            (
            Guid id, string photo, string name,
            string race, string species, Sex sex,
            DateOnly birthDate, double weight, double size,
            HistoryDTO history,UserDTOshort ownerDto
            )
        {
            Id = id;
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

        public Guid Id { get; set; }
        public string Photo { get; set; }
        public string Name { get; set; }
        public string Race { get; set; }
        public string Species { get; set; }
        public Sex Sex { get; set; }
        public DateOnly BirthDate { get; set; }
        public double Weight { get; set; }
        public double Size { get; set; }
        public HistoryDTO History { get; set; }
        public UserDTOshort OwnerDTO { get; set; }
    }

    public class PetDTOnoPhoto
    {
        public string Name { get; set; }
        public string Race { get; set; }
        public string Species { get; set; }
        public Sex Sex { get; set; }

        public PetDTOnoPhoto(string name, string race, string species, Sex sex)
        {
            Name = name;
            Race = race;
            Species = species;
            Sex = sex;
        }
    }

    public class PetDTOcreate
    {
        public string Name { get; set; }
        public string Species { get; set; }
        public string Race { get; set; }
        public double Weight { get; set; }
        public double Size { get; set; }
        public Sex Sex { get; set; }
        public DateOnly Date { get; set; }
        public string OwnerEmail { get; set; }
        public string Photo { get; set; }

        public PetDTOcreate(
            string name, string species, string race,
            double weight, double size, Sex sex,
            DateOnly date, string ownerEmail, string photo
            )
        {
            Name = name;
            Species = species;
            Race = race;
            Weight = weight;
            Size = size;
            Sex = sex;
            Date = date;
            OwnerEmail = ownerEmail;
            Photo = photo;
        }
    }

    public class PetDTOupdate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public double Size { get; set; }
        public string? Photo { get; set; }

        public PetDTOupdate(
            Guid id, string name, double weight,
            double size, string? photo
            )
        {
            Id = id;
            Name = name;
            Weight = weight;
            Size = size;
            Photo = photo;
        }
    }
}