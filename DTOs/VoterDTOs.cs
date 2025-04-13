// DTOs/VoterDTOs.cs
namespace IPLAwardManagementSystem.DTOs
{
    public class VoterCreateDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? VoterId { get; set; }
        public string? Role { get; set; }
    }

    public class VoterUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
    }

    public class VoterDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? VoterId { get; set; }
        public string? Role { get; set; }
        public DateTime JoinedDate { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
    }
}