using System;
using System.Collections.Generic;

namespace IPLAwardManagementSystem.DTOs
{
    public class MatchDTO
    {
        public int MatchId { get; set; }
        public DateTime MatchDate { get; set; }
        //public DateTime MatchTime { get; set; }
        public int VenueId { get; set; }
        public List<int> TeamIds { get; set; } = new List<int>();
    }
}
