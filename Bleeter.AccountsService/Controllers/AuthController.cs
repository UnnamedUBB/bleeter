using Bleeter.AccountService.Mediator.Commands;
using Bleeter.Shared.Utils;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bleeter.AccountService.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class AuthController : BaseController
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] SignInCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    public AuthController(IMediator mediator) : base(mediator)
    {
    }
}