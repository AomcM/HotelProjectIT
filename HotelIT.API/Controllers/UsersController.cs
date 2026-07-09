using HotelIT.API.Data;
using HotelIT.API.DTOs;
using HotelIT.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelIT.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly HotelITDbContext _context;

        public UsersController(HotelITDbContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        // POST: api/users
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDto userDto)
        {
            var user = new Users
            {
                FullName = userDto.FullName,
                Email = userDto.Email,
                PasswordHash = userDto.PasswordHash,
                Phone = userDto.Phone,
                RoleId = userDto.RoleId,
                DepartmentId = userDto.DepartmentId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userDto)
        {
            if (id != userDto.UserId)
            {
                return BadRequest("User ID mismatch.");
            }

            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.FullName = userDto.FullName;
            existingUser.Email = userDto.Email;
            existingUser.PasswordHash = userDto.PasswordHash;
            existingUser.Phone = userDto.Phone;
            existingUser.RoleId = userDto.RoleId;
            existingUser.DepartmentId = userDto.DepartmentId;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}