namespace IPLAwardManagementSystem.DTOs
{
    public class VenueDto
    {
        public int VenueId { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        public int Capacity { get; set; }
    }
    public class VenueUpdateDto
    {
        public string? Name { get; set; }
        public string? City { get; set; }
        public int? Capacity { get; set; }
    }
    public class VenueCreateDto
    {
        public string? Name { get; set; }
        public string? City { get; set; }
        public int Capacity { get; set; }
    }
}