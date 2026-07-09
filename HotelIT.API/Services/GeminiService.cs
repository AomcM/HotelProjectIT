using System.Text;
using System.Text.Json;

namespace HotelIT.API.Services
{
    public class GeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public GeminiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> AnalyzeTicket(string title, string description)
        {
            var apiKey = _configuration["Gemini:ApiKey"];

            var url =
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}";

            var prompt = $@"
You are an IT Help Desk assistant.

Analyze the following support ticket.

Title:
{title}

Description:
{description}

Respond ONLY with exactly this format:

Category: <category>
Priority: <priority>
Solution: <solution>

Do not use markdown.
Do not use bullet points.
Do not add explanations.
";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new
                            {
                                text = prompt
                            }
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(requestBody);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            using JsonDocument document = JsonDocument.Parse(result);

            var text = document
                .RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            return text ?? "";
        }
    }
}