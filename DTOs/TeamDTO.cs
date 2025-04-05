using System;
using System.Collections.Generic;

namespace IPLAwardManagementSystem.DTOs
{
    public class TeamDTO
    {
        public int TeamId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Coach { get; set; } = string.Empty;
    }
}