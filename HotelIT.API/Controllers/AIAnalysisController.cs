using HotelIT.API.Data;
using HotelIT.API.DTOs;
using HotelIT.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelIT.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIAnalysisController : ControllerBase
    {
        private readonly HotelITDbContext _context;

        public AIAnalysisController(HotelITDbContext context)
        {
            _context = context;
        }
        // GET: api/aianalysis
        [HttpGet]
        public async Task<IActionResult> GetAnalysis()
        {
            var analysis = await _context.Aianalysis.ToListAsync();

            return Ok(analysis);
        }
        // GET: api/aianalysis/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnalysisById(int id)
        {
            var analysis = await _context.Aianalysis.FindAsync(id);

            if (analysis == null)
            {
                return NotFound();
            }

            return Ok(analysis);
        }
        // POST: api/aianalysis
        [HttpPost]
        public async Task<IActionResult> CreateAnalysis([FromBody] AiAnalysisDto dto)
        {
            var analysis = new Aianalysis
            {
                TicketId = dto.TicketId,
                Category = dto.Category,
                SuggestedPriority = dto.SuggestedPriority,
                SuggestedSolution = dto.SuggestedSolution
            };

            _context.Aianalysis.Add(analysis);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAnalysisById), new { id = analysis.AnalysisId }, analysis);
        }
        // PUT: api/aianalysis/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnalysis(int id, [FromBody] AiAnalysisDto dto)
        {
            if (id != dto.AnalysisId)
            {
                return BadRequest("Analysis ID mismatch.");
            }

            var existingAnalysis = await _context.Aianalysis.FindAsync(id);

            if (existingAnalysis == null)
            {
                return NotFound();
            }

            existingAnalysis.TicketId = dto.TicketId;
            existingAnalysis.Category = dto.Category;
            existingAnalysis.SuggestedPriority = dto.SuggestedPriority;
            existingAnalysis.SuggestedSolution = dto.SuggestedSolution;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE: api/aianalysis/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnalysis(int id)
        {
            var analysis = await _context.Aianalysis.FindAsync(id);

            if (analysis == null)
            {
                return NotFound();
            }

            _context.Aianalysis.Remove(analysis);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}