namespace HotelIT.API.DTOs
{
    public class TicketDto
    {
        public int TicketId { get; set; }

        public string Title { get; set; } = "";

        public string Description { get; set; } = "";

        public string Priority { get; set; } = "";

        public string Status { get; set; } = "";

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }

        public int TechnicianId { get; set; }
    }
}