using System.Reflection.Metadata.Ecma335;
using backend.Models;
using Backend.Data;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/v1/QueryController")]

    public class QueryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IQueryService _queryService;
        public QueryController(AppDbContext context, IQueryService queryService)
        {
            _context = context;
            _queryService = queryService;
        }
        [HttpPost]
        public async Task<IActionResult> SubmitQuery([FromBody] QueryForm query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Queries.Add(query);
            await _context.SaveChangesAsync();


         _queryService.SendQueryEmailAsync(query.Name, query.Email, query.Designation, query.Phone, query.Query);



            return Ok(new { message = "Query submitted successfully." });
        }

    }
}