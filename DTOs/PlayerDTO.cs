// DTOs/PlayerDTOs.cs
namespace IPLAwardManagementSystem.DTOs
{
    public class PlayerCreateDto
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Role { get; set; }
        public int TeamId { get; set; }
    }

    public class PlayerUpdateDto
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Role { get; set; }
        public int TeamId { get; set; }
    }

    public class PlayerDto
    {
        public int PlayerId { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Role { get; set; }
        public TeamDto Team { get; set; }
        public List<AwardDto> Awards { get; set; }
    }
}