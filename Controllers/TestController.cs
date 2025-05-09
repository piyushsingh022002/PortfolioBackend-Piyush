using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
[Route("api/v1")]  // Base route for the controller

public class TestController : ControllerBase
{
    [HttpGet("test")] // GET request to /api/v1/login (temporarily use GET to test)
    public IActionResult TestRoute()
    {
        return Ok(new { message = "API is working!" });
    }
}

}