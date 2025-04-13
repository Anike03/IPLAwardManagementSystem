using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPLAwardManagementSystem.Models
{
    public class Match
    {
        [Key]
        public int MatchId { get; set; }

        [Required]
        public DateTime MatchDate { get; set; }

        public int SeasonYear { get; set; } // Added field

        // Foreign Keys
        [ForeignKey("Venue")]
        public int VenueId { get; set; }

        public int? HomeTeamId { get; set; }
        public int? AwayTeamId { get; set; }

        // Navigation properties
        public Venue? Venue { get; set; }

        [ForeignKey("HomeTeamId")]
        public Team? HomeTeam { get; set; }

        [ForeignKey("AwayTeamId")]
        public Team? AwayTeam { get; set; }

        public string Result { get; set; } = string.Empty; // Added field
    }
}