using AutoMapper;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Models;
using IPLAwardManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace IPLAwardManagementSystem.Services
{
    public class VenueTeamService : IVenueTeamService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public VenueTeamService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<VenueTeamDto> CreateAsync(VenueTeamCreateDto dto)
        {
            var entity = _mapper.Map<VenueTeam>(dto);
            _context.VenueTeams.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<VenueTeamDto>(entity);
        }

        public async Task<bool> DeleteAsync(int venueId, int teamId)
        {
            var entity = await _context.VenueTeams.FindAsync(venueId, teamId);
            if (entity != null)
            {
                _context.VenueTeams.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<VenueTeamDto>> GetAllAsync()
        {
            var list = await _context.VenueTeams
                .Include(vt => vt.Team)
                .Include(vt => vt.Venue)
                .ToListAsync();
            return _mapper.Map<List<VenueTeamDto>>(list);
        }

        public async Task<VenueTeamDto?> GetByIdAsync(int venueId, int teamId)
        {
            var entity = await _context.VenueTeams
                .Include(vt => vt.Team)
                .Include(vt => vt.Venue)
                .FirstOrDefaultAsync(vt => vt.VenueId == venueId && vt.TeamId == teamId);
            return entity == null ? null : _mapper.Map<VenueTeamDto>(entity);
        }

        public async Task<IEnumerable<VenueTeamDto>> GetTeamsForVenueAsync(int venueId)
        {
            var list = await _context.VenueTeams
                .Include(vt => vt.Team)
                .Include(vt => vt.Venue)
                .Where(vt => vt.VenueId == venueId)
                .ToListAsync();
            return _mapper.Map<List<VenueTeamDto>>(list);
        }

        public async Task<IEnumerable<VenueTeamDto>> GetVenuesForTeamAsync(int teamId)
        {
            var list = await _context.VenueTeams
                .Include(vt => vt.Team)
                .Include(vt => vt.Venue)
                .Where(vt => vt.TeamId == teamId)
                .ToListAsync();
            return _mapper.Map<List<VenueTeamDto>>(list);
        }

        public async Task<bool> IsTeamLinkedToVenueAsync(int venueId, int teamId)
        {
            return await _context.VenueTeams.AnyAsync(vt => vt.VenueId == venueId && vt.TeamId == teamId);
        }
    }
}