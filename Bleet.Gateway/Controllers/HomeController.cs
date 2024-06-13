using Microsoft.AspNetCore.Mvc;

namespace Bleet.Gateway.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{

    public IActionResult Index()
    {
        return Ok();
    }
}