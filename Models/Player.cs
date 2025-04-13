using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPLAwardManagementSystem.Models
{
    public class Player
    {
        [Key]
        public int PlayerId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = string.Empty;

        // Player statistics
        public int TotalRuns { get; set; } = 0;
        public int TotalWickets { get; set; } = 0;
        public int MatchesPlayed { get; set; } = 0;
        public decimal BattingAverage { get; set; } = 0;

        // Foreign Key for Team
        [ForeignKey("Team")]
        public int TeamId { get; set; }

        // Relationships
        public Team? Team { get; set; }
        public ICollection<PlayerAward> PlayerAwards { get; set; } = new List<PlayerAward>();
    }
}