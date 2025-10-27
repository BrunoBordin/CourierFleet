using CourierFleetApplication.Rentals.Commands;
using CourierFleetApplication.Rentals.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourierFleetApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RentalController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CreateRentalAsync([FromBody] RentalDto request)
    {
        var command = new CreateRentalCommand()
        {
            CourierId = request.CourierId,
            MotorcycleId = request.MotorcycleId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            ExpectedEndDate = request.ExpectedEndDate,
            Plan = request.Plan
        };

        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("{rentalId}/calculate-cost")]
    public async Task<IActionResult> CalculateRentalCostAsync([FromRoute] int rentalId, [FromBody] RentalCostDto request)
    {
        var command = new CalculateRentalCostCommand()
        {
            RentalId = rentalId,
            ReturnDate = request.ReturnDate
        };

        var cost = await _mediator.Send(command);
        return Ok(new { TotalCost = cost });
    }
}