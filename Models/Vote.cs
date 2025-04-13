using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPLAwardManagementSystem.Models
{
    public class Vote
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Award")]
        public int AwardId { get; set; }

        [ForeignKey("Player")]
        public int PlayerId { get; set; }

        [ForeignKey("Voter")]
        public int VoterId { get; set; }

        public DateTime VoteDate { get; set; } = DateTime.UtcNow;

        public Award? Award { get; set; }
        public Player? Player { get; set; }
        public Voter? Voter { get; set; }
    }
}