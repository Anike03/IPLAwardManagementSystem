using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPLAwardManagementSystem.Models
{
    public class Voter
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Role { get; set; } // "Fan", "Committee", "Admin"

        public DateTime JoinedDate { get; set; } = DateTime.UtcNow;
        public bool IsVerified { get; set; } = false;
        public bool IsActive { get; set; } = true;

        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}