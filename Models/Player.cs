using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

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

        // Foreign Key for Team
        [ForeignKey("Team")]
        public int TeamId { get; set; }

        // One-to-Many Relationship: A player belongs to one team
        public Team Team { get; set; } = null!;

        // Navigation property for PlayerAwards (added to fix the error)
        public ICollection<PlayerAward> PlayerAwards { get; set; } = new List<PlayerAward>();
    }
}