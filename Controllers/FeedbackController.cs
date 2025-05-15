using System.Reflection.Metadata.Ecma335;
using backend.Models;
using Backend.Data;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/v1")]

    public class FeedbackController : ControllerBase
    {
        // private readonly AppDbContext _context;
        private readonly IFeedbackService _feedbackService;
        public FeedbackController(IFeedbackService feedbackService)
        {
            // _context = context;
            _feedbackService = feedbackService;
        }
        [HttpPost("feedback")]
        public async Task<IActionResult> SubmitFeedback([FromBody] FeedbackForm feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // _context.Feedbacks.Add(feedback);
            // await _context.SaveChangesAsync();
             if (string.IsNullOrWhiteSpace(feedback.Name) ||
               string.IsNullOrWhiteSpace(feedback.Email) ||
               feedback.Rating <= 0 ||
               string.IsNullOrWhiteSpace(feedback.Feedback))
            {
                return BadRequest("All fields are required.");
            }

            await _feedbackService.SendFeedbackEmailAsync(feedback.Name, feedback.Email, feedback.Rating, feedback.Feedback);

            return Ok(new { message = "Feedback submitted successfully.Thankyou for your Consideration." });
        }

        
    }
}