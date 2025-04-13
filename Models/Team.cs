using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPLAwardManagementSystem.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }

        [Required]
        [StringLength(100)]
        public string TeamName { get; set; } = string.Empty;

        [StringLength(100)]
        public string Coach { get; set; } = string.Empty;

        public string HomeCity { get; set; } = string.Empty; // Added field
        public DateTime FoundingDate { get; set; } // Added field

        // Navigation properties
        public ICollection<Player> Players { get; set; } = new List<Player>();
        public ICollection<VenueTeam> VenueTeams { get; set; } = new List<VenueTeam>();
    }
}