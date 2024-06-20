using Bleeter.AccountsService.Mediator.Commands;
using Bleeter.Shared.Utils;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bleeter.AccountsService.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class AccountController : BaseController
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] SignInCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    
    [HttpPost("create")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    [HttpPatch]
    [Authorize]
    public async Task<IActionResult> EditAccount([FromBody] EditAccountCommand command)
    {
        await Mediator.Send(command);
        return Ok();
    }

    public AccountController(IMediator mediator) : base(mediator)
    {
    }
}