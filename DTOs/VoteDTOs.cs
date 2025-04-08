// DTOs/VoteDTOs.cs
namespace IPLAwardManagementSystem.DTOs
{
    public class VoteCreateDto
    {
        public int AwardId { get; set; }
        public int PlayerId { get; set; }
        public int VoterId { get; set; }
    }

    public class VoteDto
    {
        public int Id { get; set; }
        public int AwardId { get; set; }
        public int PlayerId { get; set; }
        public int VoterId { get; set; }
        public DateTime VoteDate { get; set; }
        public AwardDto? Award { get; set; }
        public PlayerDto? Player { get; set; }
        public VoterDto? Voter { get; set; }
    }
}