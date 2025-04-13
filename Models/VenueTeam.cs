using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPLAwardManagementSystem.Models
{
    public class VenueTeam
    {
        public int VenueId { get; set; }
        public int TeamId { get; set; }

        [ForeignKey("VenueId")]
        public Venue? Venue { get; set; }

        [ForeignKey("TeamId")]
        public Team? Team { get; set; }

        public DateTime FirstUsedDate { get; set; } // Additional relationship data
        public int MatchesPlayed { get; set; }
    }
}