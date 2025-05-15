using System.Reflection.Metadata.Ecma335;
using backend.Models;
using Backend.Data;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/v1")]

    public class QueryController : ControllerBase
    {
        // private readonly AppDbContext _context;
        private readonly IQueryService _queryService;
        public QueryController( IQueryService queryService)
        {
            // _context = context;
            _queryService = queryService;
        }
        [HttpPost("query")]
        public async Task<IActionResult> SubmitQuery([FromBody] QueryForm query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // _context.Queries.Add(query);
            // await _context.SaveChangesAsync();



            if (string.IsNullOrWhiteSpace(query.Name) ||
               string.IsNullOrWhiteSpace(query.Email) ||
               string.IsNullOrWhiteSpace(query.Designation) ||
               string.IsNullOrWhiteSpace(query.Phone) ||
               string.IsNullOrWhiteSpace(query.Query))
            {
                return BadRequest("All fields are required.");
            }


            await _queryService.SendQueryEmailAsync(query.Name, query.Email, query.Designation, query.Phone, query.Query);



            return Ok(new { message = "Query submitted successfully." });
        }

    }
}