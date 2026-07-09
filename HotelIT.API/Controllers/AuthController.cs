using HotelIT.API.Data;
using HotelIT.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelIT.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly HotelITDbContext _context;

        public AuthController(HotelITDbContext context)
        {
            _context = context;
        }
        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u =>
                    u.Email == loginDto.Email &&
                    u.PasswordHash == loginDto.PasswordHash);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(new
            {
                user.UserId,
                user.FullName,
                user.Email,
                Role = user.Role.RoleName,
                Department = user.Department.DepartmentName
            });
        }
    }
}