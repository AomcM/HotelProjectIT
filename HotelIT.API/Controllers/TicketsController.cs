using HotelIT.API.Data;
using HotelIT.API.DTOs;
using HotelIT.API.Models;
using HotelIT.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelIT.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly HotelITDbContext _context;
        private readonly GeminiService _geminiService;

        public TicketsController(
            HotelITDbContext context,
            GeminiService geminiService)
        {
            _context = context;
            _geminiService = geminiService;
        }

        // GET: api/tickets
        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            var tickets = await _context.Tickets.ToListAsync();
            return Ok(tickets);
        }

        // GET: api/tickets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        // POST: api/tickets
        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] TicketDto ticketDto)
        {
            var ticket = new Tickets
            {
                Title = ticketDto.Title,
                Description = ticketDto.Description,
                Priority = ticketDto.Priority,
                Status = ticketDto.Status,
                CreatedAt = DateTime.Now,
                UserId = ticketDto.UserId,
                TechnicianId = ticketDto.TechnicianId
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            // Call Gemini AI
            var aiResponse = await _geminiService.AnalyzeTicket(
                ticket.Title,
                ticket.Description);

            // Save AI analysis
            string category = "";
            string priority = "";
            string solution = "";

            var lines = aiResponse.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                if (line.StartsWith("Category:"))
                {
                    category = line.Replace("Category:", "").Trim();
                }
                else if (line.StartsWith("Priority:"))
                {
                    priority = line.Replace("Priority:", "").Trim();
                }
                else if (line.StartsWith("Solution:"))
                {
                    solution = line.Replace("Solution:", "").Trim();
                }
            }

            var analysis = new Aianalysis
            {
                TicketId = ticket.TicketId,
                Category = category,
                SuggestedPriority = priority,
                SuggestedSolution = solution
            };

            _context.Aianalysis.Add(analysis);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTicket), new { id = ticket.TicketId }, ticket);
        }

        // PUT: api/tickets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(int id, [FromBody] TicketDto ticketDto)
        {
            if (id != ticketDto.TicketId)
            {
                return BadRequest("Ticket ID mismatch.");
            }

            var existingTicket = await _context.Tickets.FindAsync(id);

            if (existingTicket == null)
            {
                return NotFound();
            }

            existingTicket.Title = ticketDto.Title;
            existingTicket.Description = ticketDto.Description;
            existingTicket.Priority = ticketDto.Priority;
            existingTicket.Status = ticketDto.Status;
            existingTicket.UserId = ticketDto.UserId;
            existingTicket.TechnicianId = ticketDto.TechnicianId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);

            if (ticket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}