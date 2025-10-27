namespace CourierFleetDomain.Entities;

public class Motorcycle
{
    public Motorcycle()
    { }

    public int Id { get; set; }

    public string Identifier { get; set; }

    public int Year { get; set; }

    public string Model { get; set; }

    public string LicensePlate { get; set; }

    public Motorcycle(string identifier, int year, string model, string licensePlate)
    {
        Identifier = identifier;
        Year = year;
        Model = model;
        LicensePlate = licensePlate;
    }

    public void UpdateLicensePlate(string licensePlate)
    {
        LicensePlate = licensePlate;
    }
}