using HotelIT.API.Data;
using HotelIT.API.DTOs;
using HotelIT.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelIT.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly HotelITDbContext _context;

        public AssetsController(HotelITDbContext context)
        {
            _context = context;
        }
        // GET: api/assets
        [HttpGet]
        public async Task<IActionResult> GetAssets()
        {
            var assets = await _context.Assets.ToListAsync();
            return Ok(assets);
        }
        // GET: api/assets/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsset(int id)
        {
            var asset = await _context.Assets.FindAsync(id);

            if (asset == null)
            {
                return NotFound();
            }

            return Ok(asset);
        }
        // POST: api/assets/1
        [HttpPost]
        public async Task<IActionResult> CreateAsset([FromBody] AssetDto dto)
        {
            var asset = new Assets
            {
                AssetName = dto.AssetName,
                Category = dto.Category,
                SerialNumber = dto.SerialNumber,
                PurchaseDate = dto.PurchaseDate,
                Status = dto.Status,
                Location = dto.Location,
                DepartmentId = dto.DepartmentId
            };

            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAsset), new { id = asset.AssetId }, asset);
        }
        // PUT: api/assets/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsset(int id, [FromBody] AssetDto dto)
        {
            if (id != dto.AssetId)
            {
                return BadRequest("Asset ID mismatch.");
            }

            var existingAsset = await _context.Assets.FindAsync(id);

            if (existingAsset == null)
            {
                return NotFound();
            }

            existingAsset.AssetName = dto.AssetName;
            existingAsset.Category = dto.Category;
            existingAsset.SerialNumber = dto.SerialNumber;
            existingAsset.PurchaseDate = dto.PurchaseDate;
            existingAsset.Status = dto.Status;
            existingAsset.Location = dto.Location;
            existingAsset.DepartmentId = dto.DepartmentId;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE: api/assets/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            var asset = await _context.Assets.FindAsync(id);

            if (asset == null)
            {
                return NotFound();
            }

            _context.Assets.Remove(asset);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}