using CourierFleetDomain.Enuns;

namespace CourierFleetApplication.Couriers.DTOs;

public class CourierDto
{
    public string Identifier { get; set; }
    public string Name { get; set; }
    public string Cnpj { get; set; }
    public DateTime BirthDate { get; set; }
    public string CnhNumber { get; set; }
    public ECnhType CnhType { get; set; }
}