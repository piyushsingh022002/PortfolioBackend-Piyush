using System.Reflection.Metadata.Ecma335;
using backend.Models;
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/v1")]

    public class FeedbackController : ControllerBase
    {
        private readonly AppDbContext _context;
        public FeedbackController(AppDbContext context)
        {
            _context = context;
        }
        [HttpPost("feedback")]
        public async Task<IActionResult> SubmitFeedback([FromBody] FeedbackForm feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Query submitted successfully." });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFeedback()
        {
            var feedbacks = await _context.Feedbacks.ToListAsync();
            return Ok(feedbacks);
        }
    }
}