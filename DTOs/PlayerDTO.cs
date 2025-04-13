using System.ComponentModel.DataAnnotations;

namespace IPLAwardManagementSystem.DTOs
{
    public class PlayerDto
    {
        public int PlayerId { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Role { get; set; }
        public int TeamId { get; set; }
    }
    // For creating a new player
    public class PlayerCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Range(16, 50)]
        public int Age { get; set; }

        [Required]
        public string Role { get; set; } = string.Empty;

        [Required]
        public int TeamId { get; set; }
    }

    // For updating a player
    public class PlayerUpdateDto
    {
        public int PlayerId { get; set; }  // ✅ Needed for Edit
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Role { get; set; }
        public int TeamId { get; set; }    // ✅ Needed for Team dropdown
    }


    // For listing players (basic info)
    public class PlayerListDto
    {
        public int PlayerId { get; set; }
        public string? Name { get; set; }
        public string? Role { get; set; }
        public int Age { get; set; }
        public string? TeamName { get; set; }
    }

    // For player details
    public class PlayerDetailDto
    {
        public int PlayerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Role { get; set; } = string.Empty;
        public int TeamId { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public int TotalRuns { get; set; }
        public int TotalWickets { get; set; }
        public List<PlayerAwardDto> Awards { get; set; } = new List<PlayerAwardDto>();
    }
}