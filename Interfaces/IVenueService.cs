using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Models;
using System.Collections.Generic;

namespace IPLAwardManagementSystem.Interfaces
{
    public interface IVenueService
    {
        IEnumerable<Venue> AllVenues { get; }

        Venue? GetVenueById(int id); // Add nullable return type
        void CreateVenue(VenueDTO venueDTO);
        void UpdateVenue(int id, VenueDTO venueDTO);
        void DeleteVenue(int id);
    }
}