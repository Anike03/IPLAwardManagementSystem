// Models/PlayerAward.cs (junction table for many-to-many relationship)
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IPLAwardManagementSystem.Models
{
    public class PlayerAward
    {
        public int PlayerId { get; set; }
        public int AwardId { get; set; }
        public bool IsWinner { get; set; } = false;
        public DateTime NominationDate { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Player? Player { get; set; }
        public Award? Award { get; set; }
    }
}
