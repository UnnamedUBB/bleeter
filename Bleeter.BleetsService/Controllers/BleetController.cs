using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bleeter.BleetsService.Controllers;

[Authorize]
[Route("[controller]")]
public class BleetController : ControllerBase
{
    [HttpPost]
    public IActionResult AddBleet()
    {
        return Ok();
    }

    [HttpPatch]
    public IActionResult EditBleet()
    {
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteBleet()
    {
        return Ok();
    }
}