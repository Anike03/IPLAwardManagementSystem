// DTOs/PlayerAwardDTOs.cs
namespace IPLAwardManagementSystem.DTOs
{
    public class PlayerAwardCreateDto
    {
        public int PlayerId { get; set; }
        public int AwardId { get; set; }
        public bool IsWinner { get; set; }
    }

    public class PlayerAwardUpdateDto
    {
        public bool IsWinner { get; set; }
    }

    public class PlayerAwardDto
    {
        public int PlayerId { get; set; }
        public int AwardId { get; set; }
        public bool IsWinner { get; set; }
        public DateTime NominationDate { get; set; }
        public PlayerDto? Player { get; set; }
        public AwardDto? Award { get; set; }
    }
}