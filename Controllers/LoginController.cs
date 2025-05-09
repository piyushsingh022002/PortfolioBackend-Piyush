using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/v1")]

    public class LoginController : Controller
    {
        private readonly string validUsername = "PiyushSingh";
        private readonly string validPassword = "12345";


        [HttpPost("login")]
        public IActionResult Login([FromBody] Login model)
        {
            if (model.Username == validUsername && model.Password == validPassword)
            {
                var claims = new[]{
                    new Claim(ClaimTypes.Name,model.Username)

                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("12123432345098990897654565434565"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "yourIssuer",
                    audience: "yourAudience",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new {
            success = true,
            message = "Login successful",
            token = tokenString
        });

                // return Ok(new { success = true,message = "Login successful" });
            }

            return Unauthorized(new { success = false, message = "Invalid credentials" });
        }

    }
}