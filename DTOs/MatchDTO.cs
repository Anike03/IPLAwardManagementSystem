namespace IPLAwardManagementSystem.DTOs
{
    public class MatchCreateDto
    {
        public DateTime MatchDate { get; set; }
        public int VenueId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int SeasonYear { get; set; }
    }

    public class MatchUpdateDto
    {
        public DateTime MatchDate { get; set; }
        public int VenueId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
    }

    public class MatchDto
    {
        public int MatchId { get; set; }
        public DateTime MatchDate { get; set; }
        public int SeasonYear { get; set; }
        public int VenueId { get; set; }
        public string VenueName { get; set; } = string.Empty;
        public int HomeTeamId { get; set; }
        public string HomeTeamName { get; set; } = string.Empty;
        public int AwayTeamId { get; set; }
        public string AwayTeamName { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
    }
}