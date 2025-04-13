using System.ComponentModel.DataAnnotations;

namespace IPLAwardManagementSystem.DTOs
{
    // For assigning a venue to a team (POST)
    public class VenueTeamCreateDto
    {
        [Required]
        public int VenueId { get; set; }

        [Required]
        public int TeamId { get; set; }
    }

    // For response (GET)
    public class VenueTeamDto
    {
        public int VenueId { get; set; }
        public string VenueName { get; set; } = string.Empty;
        public int TeamId { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public int MatchesPlayed { get; set; }
    }
}