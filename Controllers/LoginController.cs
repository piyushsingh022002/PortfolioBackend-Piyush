using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/v1/login")]

    public class LoginController : Controller
    {
        private readonly string validUsername = "PiyushSingh";
        private readonly string validPassword = "12345";


        [HttpPost("login")]
        public IActionResult Login([FromBody] Login model)
        {
            if (model.Username == validUsername && model.Password == validPassword)
            {
                return Ok(new { success = true,message = "Login successful" });
            }

            return Unauthorized(new {success = false, message = "Invalid credentials" });
        }

    }
}