using HotelIT.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelIT.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeminiController : ControllerBase
    {
        private readonly GeminiService _geminiService;

        public GeminiController(GeminiService geminiService)
        {
            _geminiService = geminiService;
        }

        [HttpGet]
        public async Task<IActionResult> TestGemini()
        {
            var result = await _geminiService.AnalyzeTicket(
                "Printer not working",
                "The printer in reception does not print invoices."
            );

            return Ok(result);
        }
    }
}