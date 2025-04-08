// DTOs/MatchDTOs.cs
namespace IPLAwardManagementSystem.DTOs
{
    public class MatchCreateDto
    {
        public DateTime MatchDate { get; set; }
        public int VenueId { get; set; }
        public List<int>? TeamIds { get; set; }
    }

    public class MatchUpdateDto
    {
        public DateTime MatchDate { get; set; }
        public int VenueId { get; set; }
        public List<int>? TeamIds { get; set; }
    }

    public class MatchDto
    {
        public int MatchId { get; set; }
        public DateTime MatchDate { get; set; }
        public VenueDto Venue { get; set; }
        public List<TeamDto>? Teams { get; set; }
    }
}