using Microsoft.AspNetCore.Mvc;
using Hangfire;
using Backend.Data;
using Backend.Models;
using Backend.Services;

[ApiController]
[Route("api/v1")]
public class ContactController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public ContactController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("contact")]
    public IActionResult SubmitContact([FromBody] ContactForm contact, [FromServices] IEmailService emailService)
    {
        _dbContext.Contacts.Add(contact);
        _dbContext.SaveChanges();

        // Enqueue background email job using Hangfire
        if (string.IsNullOrWhiteSpace(contact.Name) ||
        string.IsNullOrWhiteSpace(contact.Email) ||
        string.IsNullOrWhiteSpace(contact.Message))
        {
            return BadRequest("All fields are required.");
        }
        BackgroundJob.Enqueue(() => emailService.SendContactEmailAsync(contact.Name, contact.Email, contact.Message));

        return Ok(new { message = "Contact submitted successfully." });
    }
}
