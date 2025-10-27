using CourierFleetApplication.Motorcycles.Commands;
using CourierFleetApplication.Motorcycles.DTOs;
using CourierFleetApplication.Motorcycles.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourierFleetApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MotorcyclesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CreateMotorcyclesAsync([FromBody] MotorcycleDto request)
    {
        var command = new CreateMotorcycleCommand()
        {
            Identifier = request.Identifier,
            Year = request.Year,
            Model = request.Model,
            LicensePlate = request.LicensePlate
        };

        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetMotorcyclesAsync([FromQuery] string? licensePlate)
    {
        var query = new GetMotorcyclesQuery()
        {
            LicensePlate = licensePlate
        };

        var motorcycles = await _mediator.Send(query);
        return Ok(motorcycles);
    }

    [HttpPut("{motorcycleId}/license-plate")]
    public async Task<IActionResult> UpdateLicensePlateAsync([FromRoute] int motorcycleId, [FromBody] UpdateLicensePlateDto request)
    {
        var command = new UpdateMotorcycleLicensePlateCommand()
        {
            MotorcycleId = motorcycleId,
            NewLicensePlate = request.NewLicensePlate
        };

        await _mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{motorcycleId}")]
    public async Task<IActionResult> DeleteMotorcycleAsync([FromRoute] int motorcycleId)
    {
        var command = new DeleteMotorcycleCommand()
        {
            MotorcycleId = motorcycleId
        };

        await _mediator.Send(command);
        return Ok();
    }
}