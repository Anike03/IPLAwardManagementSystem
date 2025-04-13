using System.ComponentModel.DataAnnotations;

namespace IPLAwardManagementSystem.DTOs
{
    public class AwardCreateDto
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int SeasonYear { get; set; } = DateTime.Now.Year;
    }

    public class AwardUpdateDto
    {
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class AwardDto
    {
        public int AwardId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int SeasonYear { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}