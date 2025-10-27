using CourierFleetDomain.Enuns;

namespace CourierFleetApplication.Rentals.DTOs;

public class RentalDto
{
    public int CourierId { get; set; }

    public int MotorcycleId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime ExpectedEndDate { get; set; }

    public ERentalPlan Plan { get; set; }
}