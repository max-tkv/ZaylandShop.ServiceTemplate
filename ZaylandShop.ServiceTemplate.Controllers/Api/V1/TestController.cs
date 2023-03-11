using Microsoft.AspNetCore.Mvc;
using ZaylandShop.ServiceTemplate.Controllers.Models;

namespace ZaylandShop.ServiceTemplate.Controllers.Api.V1;

[ApiVersion("1")]
[Route("v{version:apiVersion}/[controller]")]
[ApiController]
public class TestController : Controller
{

    public TestController()
    {
    }

    [HttpGet]
    public IActionResult Test(string data)
    {
        return Ok(new Test()
        {
            Name = data
        });
    }
}