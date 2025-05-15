using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services;

[ApiController]
[Route("api/v1")]
public class ContactController : ControllerBase
{
    // private readonly AppDbContext _dbContext;
    private readonly IEmailService _emailService;

    public ContactController(IEmailService emailService)
    {
        // _dbContext = dbContext;
        _emailService = emailService;
    }

    [HttpPost("contact")]
    public async Task<IActionResult> SubmitContact([FromBody] ContactForm contact)
    {
        // _dbContext.Contacts.Add(contact);
        // _dbContext.SaveChanges();

        // Enqueue background email job using Hangfire
        if (string.IsNullOrWhiteSpace(contact.Name) ||
        string.IsNullOrWhiteSpace(contact.Email) ||
        string.IsNullOrWhiteSpace(contact.Message))
        {
            return BadRequest("All fields are required.");
        }
        // BackgroundJob.Enqueue(() => emailService.SendContactEmailAsync(contact.Name, contact.Email, contact.Message));
         await _emailService.SendContactEmailAsync(contact.Name, contact.Email, contact.Message);


        return Ok(new { message = "Contact submitted successfully.I will Follow Up very Soon.Thankyou" });
    }
}
