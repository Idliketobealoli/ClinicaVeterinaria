namespace ClinicaVeterinaria.API.Api.model;

public class Vaccine
{
    public Vaccine(string name, DateOnly date)
    {
        Name = name;
        Date = date;
    }

    public string Name { get; set; }
    public DateOnly Date { get; set; }
}