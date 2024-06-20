using Bleeter.BleetsService.Mediator.Commands;
using Bleeter.BleetsService.Mediator.Queries;
using Bleeter.Shared.Utils;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bleeter.BleetsService.Controllers;

[Authorize]
[Route("[controller]")]
public class BleetController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetBleets([FromQuery] GetBleetsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    
    [HttpGet("user")]
    public async Task<IActionResult> GetBleets([FromQuery] GetBleetsByAuthorIdQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
    
    [HttpPost]
    public async Task<IActionResult> AddBleet([FromBody] AddBleetCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBleet([FromQuery] DeleteBleetCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    public BleetController(IMediator mediator) : base(mediator)
    {
    }
}