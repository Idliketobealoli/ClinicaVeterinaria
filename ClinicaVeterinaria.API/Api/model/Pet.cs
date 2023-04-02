namespace ClinicaVeterinaria.API.Api.model;

internal class Pet
{
    public Pet(string name, string species, string race, double weight, double size, Sex sex, DateOnly birthDate,
        string owner, string ownerEmail, History history, string photo)
    {
        Id = Guid.NewGuid();
        Name = name;
        Species = species;
        Race = race;
        Weight = weight;
        Size = size;
        Sex = sex;
        BirthDate = birthDate;
        Owner = owner;
        OwnerEmail = ownerEmail;
        History = history;
        Photo = photo;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Species { get; set; }
    public string Race { get; set; }
    public double Weight { get; set; }
    public double Size { get; set; }
    public Sex Sex { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Owner { get; set; }
    public string OwnerEmail { get; set; }
    public History History { get; set; }
    public string Photo { get; set; }
}

enum Sex
{
    MALE,
    FEMALE
}