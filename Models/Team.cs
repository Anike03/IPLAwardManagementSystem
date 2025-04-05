using System.Numerics;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IPLAwardManagementSystem.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public string Coach { get; set; } = string.Empty;

        // Navigation properties
        //public ICollection<Player> Players { get; set; } = new List<Player>();
        public ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}


