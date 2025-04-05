// Models/Award.cs
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IPLAwardManagementSystem.Models

{
    public class Award
    {
        public int Id { get; set; }
        public string? Name { get; set; } // e.g., "Best Batsman", "Purple Cap"
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public ICollection<PlayerAward>? PlayerAwards { get; set; }
        public ICollection<Vote>? Votes { get; set; }
        //public object? PlayerAwards { get; internal set; }
    }
}

