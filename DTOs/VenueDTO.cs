// DTOs/VenueDTOs.cs
namespace IPLAwardManagementSystem.DTOs
{
    public class VenueCreateDto
    {
        public string Name { get; set; }
        public string Location { get; set; }
    }

    public class VenueUpdateDto
    {
        public string Name { get; set; }
        public string Location { get; set; }
    }

    public class VenueDto
    {
        public int VenueId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
    }
}