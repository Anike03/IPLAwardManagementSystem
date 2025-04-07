// DTOs/VoterDTOs.cs
namespace IPLAwardManagementSystem.DTOs
{
    public class VoterCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? VoterId { get; set; }
        public string Role { get; set; } = string.Empty;
    }

    public class VoterUpdateDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsActive { get; set; }
    }

    public class VoterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? VoterId { get; set; }
        public string Role { get; set; } = string.Empty;
        public DateTime JoinedDate { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
    }
}