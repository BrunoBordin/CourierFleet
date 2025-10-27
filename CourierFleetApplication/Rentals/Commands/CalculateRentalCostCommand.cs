using CourierFleetDomain.Entities;
using CourierFleetDomain.Enuns;
using CourierFleetDomain.Interfaces;
using MediatR;
using System.Numerics;

namespace CourierFleetApplication.Rentals.Commands;

public class CalculateRentalCostCommand : IRequest<decimal>
{
    public int RentalId { get; set; }
    public DateTime ReturnDate { get; set; }
}

public class CalculateRentalCostCommandHandler(IRentalRepository rentalRepository) : IRequestHandler<CalculateRentalCostCommand, decimal>
{
    private readonly IRentalRepository _rentalRepository = rentalRepository;

    public async Task<decimal> Handle(CalculateRentalCostCommand request, CancellationToken cancellationToken)
    {
        var rental = await _rentalRepository.GetByIdAsync(request.RentalId);

        return rental == null ? throw new Exception("Rental not found.") : CalculateTotalCost(rental, request);
    }

    private static decimal CalculateTotalCost(Rental rental, CalculateRentalCostCommand request)
    {
        var plan = RentalPlans.GetPlan(rental.Plan);

        if (request.ReturnDate < rental.StartDate)
            throw new Exception("Return date cannot be before start date.");

        if (request.ReturnDate < rental.ExpectedEndDate)
        {
            var daysRented = (request.ReturnDate - rental.StartDate).Days;
            var dailyCost = plan.DailyCost;
            var totalCost = daysRented * dailyCost;

            var daysNotEffected = (rental.ExpectedEndDate - request.ReturnDate).Days;
            var penalty = daysNotEffected * dailyCost * plan.PenaltyPercentage;

            return totalCost + penalty;
        }
        else if (request.ReturnDate > rental.ExpectedEndDate)
        {
            var daysRented = (rental.ExpectedEndDate - rental.StartDate).Days;
            var dailyCost = plan.DailyCost;
            var totalCost = daysRented * dailyCost;

            var additionalDays = (request.ReturnDate - rental.ExpectedEndDate).Days;
            var additionalCost = additionalDays * 50.00m;

            return totalCost + additionalCost;
        }
        else
        {
            var daysRented = (rental.ExpectedEndDate - rental.StartDate).Days;
            var dailyCost = plan.DailyCost;
            return daysRented * dailyCost;
        }
    }
}