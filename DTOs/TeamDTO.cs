using System.ComponentModel.DataAnnotations;

namespace IPLAwardManagementSystem.DTOs
{
    public class TeamDto
    {
        public int TeamId { get; set; }
        public string? TeamName { get; set; }
        public string? Coach { get; set; }
        public string? HomeCity { get; set; }
        public DateTime FoundingDate { get; set; }
    }
    public class TeamCreateDto
    {
        [Required]
        public string TeamName { get; set; } = string.Empty;
        public string Coach { get; set; } = string.Empty;
        public string HomeCity { get; set; } = string.Empty;
        public DateTime FoundingDate { get; set; }
    }

    public class TeamUpdateDto
    {
        public int TeamId { get; set; } // ✅ Add this line
        public string? TeamName { get; set; }
        public string? Coach { get; set; }
        public string? HomeCity { get; set; }
        public DateTime FoundingDate { get; set; }
    }

    public class TeamListDto
    {
        public int TeamId { get; set; }
        public string? TeamName { get; set; }
        public string? Coach { get; set; }
        public string? HomeCity { get; set; } // ✅ Add this line
        public int PlayerCount { get; set; }
    }

    public class TeamDetailDto
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public string Coach { get; set; } = string.Empty;
        public string HomeCity { get; set; } = string.Empty;
        public DateTime FoundingDate { get; set; }
        public List<PlayerListDto> Players { get; set; } = new List<PlayerListDto>();
    }
}