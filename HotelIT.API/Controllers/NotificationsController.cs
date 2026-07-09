using HotelIT.API.Data;
using HotelIT.API.DTOs;
using HotelIT.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelIT.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly HotelITDbContext _context;

        public NotificationsController(HotelITDbContext context)
        {
            _context = context;
        }
        // GET: api/notifications
        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            var notifications = await _context.Notification.ToListAsync();
            return Ok(notifications);
        }
        // GET: api/notifications/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotification(int id)
        {
            var notification = await _context.Notification.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }
        // POST: api/notifications
        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] NotificationDto dto)
        {
            var notification = new Notification
            {
                UserId = dto.UserId,
                Title = dto.Title,
                Message = dto.Message,
                IsRead = dto.IsRead,
                CreatedAt = dto.CreatedAt
            };

            _context.Notification.Add(notification);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNotification), new { id = notification.NotificationId }, notification);
        }
        // PUT: api/notifications/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNotification(int id, [FromBody] NotificationDto dto)
        {
            if (id != dto.NotificationId)
            {
                return BadRequest("Notification ID mismatch.");
            }

            var existingNotification = await _context.Notification.FindAsync(id);

            if (existingNotification == null)
            {
                return NotFound();
            }

            existingNotification.UserId = dto.UserId;
            existingNotification.Title = dto.Title;
            existingNotification.Message = dto.Message;
            existingNotification.IsRead = dto.IsRead;
            existingNotification.CreatedAt = dto.CreatedAt;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE: api/notifications/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var notification = await _context.Notification.FindAsync(id);

            if (notification == null)
            {
                return NotFound();
            }

            _context.Notification.Remove(notification);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
    
