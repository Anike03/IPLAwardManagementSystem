// DTOs/PlayerAwardDTOs.cs
namespace IPLAwardManagementSystem.DTOs
{
    public class PlayerAwardCreateDto
    {
        public int PlayerId { get; set; }
        public int AwardId { get; set; }
        public bool IsWinner { get; set; } = false;
    }

    public class PlayerAwardUpdateDto
    {
        public bool? IsWinner { get; set; }
    }

    public class PlayerAwardDto
    {
        public int PlayerId { get; set; }
        public int AwardId { get; set; }
        public bool IsWinner { get; set; }
        public DateTime NominationDate { get; set; }

        // Optional: Include related data if needed
        public string? PlayerName { get; set; }
        public string? AwardName { get; set; }
    }
}