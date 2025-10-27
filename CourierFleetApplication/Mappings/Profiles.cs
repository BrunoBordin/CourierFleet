using AutoMapper;
using CourierFleetApplication.Couriers.DTOs;
using CourierFleetApplication.Motorcycles.DTOs;
using CourierFleetApplication.Rentals.DTOs;
using CourierFleetDomain.Entities;

namespace CourierFleetApplication.Mappings;

public class Profiles : Profile
{
    public Profiles()
    {
        CreateMap<Motorcycle, MotorcycleDto>().ReverseMap();
        CreateMap<Courier, CourierDto>().ReverseMap();
        CreateMap<Rental, RentalDto>().ReverseMap();
    }
}