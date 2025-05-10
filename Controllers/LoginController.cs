using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly string validUsername = "PiyushSingh";
        private readonly string validPassword = "12345";

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Login model)
        {
            if (model.Username == validUsername && model.Password == validPassword)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, model.Username)
                };

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key missing"))
                );

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: creds
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new
                {
                    success = true,
                    message = "Login successful",
                    token = tokenString
                });
            }

            return Unauthorized(new
            {
                success = false,
                message = "Invalid credentials"
            });
        }
    }
}
