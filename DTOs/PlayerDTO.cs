// DTOs/PlayerDTO.cs
using System;
using System.Collections.Generic;

namespace IPLAwardManagementSystem.DTOs
{
    public class PlayerDTO
    {
        public int PlayerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Role { get; set; } = string.Empty;
        public int TeamId { get; set; }

        // Optional: Include if you need to show related data
        public TeamDTO? Team { get; set; }
        //public ICollection<PlayerAwardDTO>? PlayerAwards { get; set; }
    }
}