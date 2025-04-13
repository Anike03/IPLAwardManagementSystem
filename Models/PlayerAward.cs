using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPLAwardManagementSystem.Models
{
    public class PlayerAward
    {
        [ForeignKey("Player")]
        public int PlayerId { get; set; }

        [ForeignKey("Award")]
        public int AwardId { get; set; }

        public bool IsWinner { get; set; } = false;
        public DateTime NominationDate { get; set; } = DateTime.UtcNow;
        public int VotesReceived { get; set; } = 0;

        public Player? Player { get; set; }
        public Award? Award { get; set; }
    }
}