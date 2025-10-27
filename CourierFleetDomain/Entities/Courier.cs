using CourierFleetDomain.Enuns;

namespace CourierFleetDomain.Entities;

public class Courier
{
    public int Id { get; set; }
    public string Identifier { get; set; }
    public string Name { get; set; }
    public string Cnpj { get; set; }
    public DateTime BirthDate { get; set; }
    public string CnhNumber { get; set; }
    public ECnhType CnhType { get; set; }
    public string? CnhImage { get; set; }

    public Courier()
    { }

    public Courier(string identifier, string name, string cnpj, DateTime birthDate, string cnhNumber, ECnhType cnhType)
    {
        Identifier = identifier;
        Name = name;
        Cnpj = cnpj;
        BirthDate = birthDate;
        CnhNumber = cnhNumber;
        CnhType = cnhType;
    }

    public bool CanRentMotorcycle()
    {
        return CnhType == ECnhType.A || CnhType == ECnhType.AB;
    }

    public void UpdateCnhImage(string cnhImage)
    {
        CnhImage = cnhImage;
    }
}