namespace HotelIT.API.DTOs
{
    public class AiAnalysisDto
    {
        public int AnalysisId { get; set; }

        public int TicketId { get; set; }

        public string Category { get; set; } = "";

        public string SuggestedPriority { get; set; } = "";

        public string SuggestedSolution { get; set; } = "";
    }
}