using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPLAwardManagementSystem.Models
{
    public class Award
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        public string? Description { get; set; }
        public int SeasonYear { get; set; } // Added field
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public ICollection<PlayerAward> PlayerAwards { get; set; } = new List<PlayerAward>();
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}