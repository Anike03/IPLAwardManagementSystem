// DTOs/TeamDTOs.cs
namespace IPLAwardManagementSystem.DTOs
{
    public class TeamCreateDto
    {
        public string TeamName { get; set; }
        public string Coach { get; set; }
    }

    public class TeamUpdateDto
    {
        public string TeamName { get; set; }
        public string Coach { get; set; }
    }

    public class TeamDto
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string Coach { get; set; }
        public List<PlayerDto> Players { get; set; }
    }
}