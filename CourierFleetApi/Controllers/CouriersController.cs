using CourierFleetApplication.Couriers.Commands;
using CourierFleetApplication.Couriers.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CourierFleetApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CouriersController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> CreateCourierAsync([FromBody] CourierDto request)
    {
        var command = new CreateCourierCommand()
        {
            Identifier = request.Identifier,
            Name = request.Name,
            Cnpj = request.Cnpj,
            BirthDate = request.BirthDate,
            CnhNumber = request.CnhNumber,
            CnhType = request.CnhType
        };

        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPost("{courierId}/cnh-image")]
    public async Task<IActionResult> UploadCnhImageAsync([FromRoute] int courierId, IFormFile file)
    {
        var command = new UploadCnhImageCommand()
        {
            CourierId = courierId,
            File = file
        };

        await _mediator.Send(command);
        return Ok();
    }
}