using IPLAwardManagementSystem.DTOs;

namespace IPLAwardManagementSystem.Services
{
    public interface IVenueTeamService
    {
        Task<IEnumerable<VenueTeamDto>> GetAllAsync();
        Task<VenueTeamDto?> GetByIdAsync(int venueId, int teamId);
        Task<VenueTeamDto> CreateAsync(VenueTeamCreateDto dto);
        Task<bool> DeleteAsync(int venueId, int teamId);

        // Extended operations
        Task<IEnumerable<VenueTeamDto>> GetTeamsForVenueAsync(int venueId);
        Task<IEnumerable<VenueTeamDto>> GetVenuesForTeamAsync(int teamId);
        Task<bool> IsTeamLinkedToVenueAsync(int venueId, int teamId);
    }
}