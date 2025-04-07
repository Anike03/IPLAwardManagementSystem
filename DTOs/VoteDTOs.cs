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

        // Optional: Include related data if needed
        public string? AwardName { get; set; }
        public string? PlayerName { get; set; }
        public string? VoterName { get; set; }
    }
}