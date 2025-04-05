// Models/Voter.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPLAwardManagementSystem.Models

{
    public class Voter
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? VoterId { get; set; } // Unique identifier for voters
        public string? Role { get; set; } // "Fan", "Committee", "Admin"
        public DateTime JoinedDate { get; set; } = DateTime.UtcNow;
        public bool IsVerified { get; set; } = false;
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public ICollection<Vote>? Votes { get; set; }
    }
}
