using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IPLAwardManagementSystem.Models; // ✅ Ensure this matches your project structure

namespace IPLAwardManagementSystem.Models
{
    public class PlayerAward
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }
        public Player? Player { get; set; }

        public int AwardId { get; set; }
        public Award? Award { get; set; }

        public DateTime Year { get; set; }

        public bool IsWinner { get; set; }
        public DateTime NominationDate { get; internal set; }
        public int VotesReceived { get; internal set; }
    }
}
