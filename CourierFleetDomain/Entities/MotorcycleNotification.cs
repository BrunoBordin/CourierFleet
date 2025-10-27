namespace CourierFleetDomain.Entities;

public class MotorcycleNotification
{
    public int Id { get; set; }

    public int MotorcycleId { get; set; }

    public string Identifier { get; set; }

    public int Year { get; set; }

    public string Model { get; set; }

    public string LicensePlate { get; set; }

    public DateTime NotificationDate { get; set; }
}