// Models/Vote.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace IPLAwardManagementSystem.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public int AwardId { get; set; }
        public int PlayerId { get; set; }
        public int VoterId { get; set; }
        public DateTime VoteDate { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Award? Award { get; set; }
        public Player? Player { get; set; }
        public Voter? Voter { get; set; }
    }
}

