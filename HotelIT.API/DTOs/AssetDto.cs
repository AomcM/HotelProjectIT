namespace HotelIT.API.DTOs
{
    public class AssetDto
    {
        public int AssetId { get; set; }

        public string AssetName { get; set; } = "";

        public string Category { get; set; } = "";

        public string SerialNumber { get; set; } = "";

        public DateOnly PurchaseDate { get; set; }

        public string Status { get; set; } = "";

        public string Location { get; set; } = "";

        public int DepartmentId { get; set; }
    }
}