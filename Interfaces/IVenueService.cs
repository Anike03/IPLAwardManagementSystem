// Interfaces/IVenueService.cs
using IPLAwardManagementSystem.DTOs;

namespace IPLAwardManagementSystem.Interfaces
{
    public interface IVenueService
    {
        Task<VenueDto> CreateVenueAsync(VenueCreateDto venueCreateDto);
        Task<IEnumerable<VenueDto>> GetAllVenuesAsync();
        Task<VenueDto> GetVenueByIdAsync(int id);
        Task UpdateVenueAsync(int id, VenueUpdateDto venueUpdateDto);
        Task DeleteVenueAsync(int id);
        Task<IEnumerable<MatchDto>> GetMatchesAtVenueAsync(int venueId);
    }
}