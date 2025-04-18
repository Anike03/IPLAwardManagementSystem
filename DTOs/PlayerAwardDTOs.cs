using System.ComponentModel.DataAnnotations;

namespace IPLAwardManagementSystem.DTOs
{
    public class PlayerAwardCreateDto
    {
        public int PlayerId { get; set; }
        public int AwardId { get; set; }
        public DateTime NominationDate { get; set; } = DateTime.UtcNow;
        public bool IsWinner { get; set; } = false;
        public int VotesReceived { get; set; } = 0;
    }

    public class PlayerAwardUpdateDto
    {
        public bool IsWinner { get; set; }
    }

    public class PlayerAwardDto
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; } = string.Empty;
        public int AwardId { get; set; }
        public string AwardName { get; set; } = string.Empty;
        public bool IsWinner { get; set; }
        public DateTime NominationDate { get; set; }
        public int VotesReceived { get; set; }
        public int Year { get; set; } 
    }
}