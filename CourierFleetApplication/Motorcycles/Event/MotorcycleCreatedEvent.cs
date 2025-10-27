namespace CourierFleetApplication.Motorcycles.Event;

public class MotorcycleCreatedEvent
{
    public int Id { get; set; }

    public string Identifier { get; set; }

    public int Year { get; set; }

    public string Model { get; set; }

    public string LicensePlate { get; set; }
}