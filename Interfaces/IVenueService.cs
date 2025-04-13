using IPLAwardManagementSystem.DTOs;

namespace IPLAwardManagementSystem.Services
{
    public interface IVenueService
    {
        Task<VenueDto?> GetVenueByIdAsync(int id);
        Task<IEnumerable<VenueDto>> GetAllVenuesAsync();
        Task<VenueDto> CreateVenueAsync(VenueCreateDto venueDto);
        Task UpdateVenueAsync(int id, VenueUpdateDto venueDto);
        Task DeleteVenueAsync(int id);

        Task AssignTeamToVenueAsync(int venueId, int teamId);
        Task RemoveTeamFromVenueAsync(int venueId, int teamId);
        Task UpdateVenueTeamStatsAsync(int venueId, int teamId, int matchesPlayed);

        Task<IEnumerable<VenueTeamDto>> GetTeamsForVenueAsync(int venueId);
        Task<IEnumerable<VenueDto>> GetVenuesForTeamAsync(int teamId);
        Task<VenueStatsDto> GetVenueStatsAsync(int venueId);
        Task<bool> IsTeamAssignedToVenueAsync(int venueId, int teamId);

        Task ScheduleMatchAtVenueAsync(int venueId, int homeTeamId, int awayTeamId, DateTime matchDate);
    }

    public class VenueStatsDto
    {
        public int TotalMatchesHosted { get; set; }
        public int UniqueTeamsHosted { get; set; }
        public DateTime LastMatchDate { get; set; }
    }
}