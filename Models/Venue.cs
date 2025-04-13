using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPLAwardManagementSystem.Models
{
    public class Venue
    {
        [Key]
        public int VenueId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string City { get; set; } = string.Empty;

        public int Capacity { get; set; } // Added field

        // One-to-Many: Venue to Matches
        public ICollection<Match> Matches { get; set; } = new List<Match>();

        // Many-to-Many: Venue to Teams (new relationship)
        public ICollection<VenueTeam> VenueTeams { get; set; } = new List<VenueTeam>();
    }
}