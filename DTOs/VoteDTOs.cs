﻿namespace IPLAwardManagementSystem.DTOs
{
    public class VoteCreateDto
    {
        public int PlayerId { get; set; }
        public int AwardId { get; set; }
        public int VoterId { get; set; }
        public DateTime VoteDate { get; set; }
    }

    public class VoteDto
    {
        public int Id { get; set; }

        // ✅ Add these for controller compatibility
        public int PlayerId { get; set; }
        public int AwardId { get; set; }
        public int VoterId { get; set; }

        public string? PlayerName { get; set; }
        public string? AwardName { get; set; }
        public string? VoterName { get; set; }
        public DateTime VoteDate { get; set; }
    }

    public class VoteUpdateDto
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int AwardId { get; set; }
        public int VoterId { get; set; }
        public DateTime VoteDate { get; set; }
    }
    // VoteResultDto.cs
    public class VoteResultDto
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; } = string.Empty;

        public int AwardId { get; set; }
        public string AwardName { get; set; } = string.Empty;

        public int TotalVotes { get; set; }
        public string Status { get; set; } = "Nominated";
    }


}
