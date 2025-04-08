using IPLAwardManagementSystem.Data;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Interfaces;
using IPLAwardManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace IPLAwardManagementSystem.Services
{
    public class VenueService : IVenueService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public VenueService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VenueDto> CreateVenueAsync(VenueCreateDto venueCreateDto)
        {
            var venue = _mapper.Map<Venue>(venueCreateDto);
            _context.Venues.Add(venue);
            await _context.SaveChangesAsync();
            return _mapper.Map<VenueDto>(venue);
        }

        public async Task<IEnumerable<VenueDto>> GetAllVenuesAsync()
        {
            var venues = await _context.Venues.ToListAsync();
            return _mapper.Map<IEnumerable<VenueDto>>(venues);
        }

        public async Task<VenueDto> GetVenueByIdAsync(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null) throw new KeyNotFoundException("Venue not found");
            return _mapper.Map<VenueDto>(venue);
        }

        public async Task UpdateVenueAsync(int id, VenueUpdateDto venueUpdateDto)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null) throw new KeyNotFoundException("Venue not found");

            _mapper.Map(venueUpdateDto, venue);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVenueAsync(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue == null) throw new KeyNotFoundException("Venue not found");

            _context.Venues.Remove(venue);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MatchDto>> GetMatchesAtVenueAsync(int venueId)
        {
            var matches = await _context.Matches
                .Where(m => m.VenueId == venueId)
                .Include(m => m.Teams)
                .ToListAsync();

            return _mapper.Map<IEnumerable<MatchDto>>(matches);
        }
    }
}