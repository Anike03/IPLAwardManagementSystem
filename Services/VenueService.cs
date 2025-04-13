using AutoMapper;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Models;
using IPLAwardManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task<VenueDto> CreateVenueAsync(VenueCreateDto dto)
        {
            var venue = _mapper.Map<Venue>(dto);
            _context.Venues.Add(venue);
            await _context.SaveChangesAsync();
            return _mapper.Map<VenueDto>(venue);
        }

        public async Task DeleteVenueAsync(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue != null)
            {
                _context.Venues.Remove(venue);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<VenueDto>> GetAllVenuesAsync()
        {
            var venues = await _context.Venues.ToListAsync();
            return _mapper.Map<List<VenueDto>>(venues);
        }

        public async Task<VenueDto?> GetVenueByIdAsync(int id)
        {
            var venue = await _context.Venues.FindAsync(id);
            return venue == null ? null : _mapper.Map<VenueDto>(venue);
        }

        public async Task<IEnumerable<VenueDto>> GetVenuesForTeamAsync(int teamId)
        {
            var venues = await _context.VenueTeams
                .Where(vt => vt.TeamId == teamId)
                .Select(vt => vt.Venue)
                .ToListAsync();
            return _mapper.Map<List<VenueDto>>(venues);
        }
        public async Task UpdateVenueAsync(int id, VenueUpdateDto venueDto)
        {
            var venue = await _context.Venues.FindAsync(id);
            if (venue != null)
            {
                _mapper.Map(venueDto, venue);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<IEnumerable<VenueTeamDto>> GetTeamsForVenueAsync(int venueId)
        {
            var teams = await _context.VenueTeams
                .Where(vt => vt.VenueId == venueId)
                .Include(vt => vt.Team)
                .Include(vt => vt.Venue)
                .ToListAsync();
            return _mapper.Map<List<VenueTeamDto>>(teams);
        }

        public async Task<bool> IsTeamAssignedToVenueAsync(int venueId, int teamId)
        {
            return await _context.VenueTeams.AnyAsync(vt => vt.VenueId == venueId && vt.TeamId == teamId);
        }

        public async Task AssignTeamToVenueAsync(int venueId, int teamId)
        {
            if (!await IsTeamAssignedToVenueAsync(venueId, teamId))
            {
                _context.VenueTeams.Add(new VenueTeam
                {
                    VenueId = venueId,
                    TeamId = teamId,
                    FirstUsedDate = DateTime.UtcNow,
                    MatchesPlayed = 0
                });
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveTeamFromVenueAsync(int venueId, int teamId)
        {
            var vt = await _context.VenueTeams.FindAsync(venueId, teamId);
            if (vt != null)
            {
                _context.VenueTeams.Remove(vt);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateVenueTeamStatsAsync(int venueId, int teamId, int matchesPlayed)
        {
            var vt = await _context.VenueTeams.FindAsync(venueId, teamId);
            if (vt != null)
            {
                vt.MatchesPlayed = matchesPlayed;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ScheduleMatchAtVenueAsync(int venueId, int homeTeamId, int awayTeamId, DateTime matchDate)
        {
            _context.Matches.Add(new Match
            {
                VenueId = venueId,
                HomeTeamId = homeTeamId,
                AwayTeamId = awayTeamId,
                MatchDate = matchDate,
                SeasonYear = matchDate.Year
            });
            await _context.SaveChangesAsync();
        }

        public async Task<VenueStatsDto> GetVenueStatsAsync(int venueId)
        {
            var matches = await _context.Matches
                .Where(m => m.VenueId == venueId)
                .ToListAsync();

            var uniqueTeams = new HashSet<int>();
            foreach (var match in matches)
            {
                if (match.HomeTeamId.HasValue) uniqueTeams.Add(match.HomeTeamId.Value);
                if (match.AwayTeamId.HasValue) uniqueTeams.Add(match.AwayTeamId.Value);
            }

            return new VenueStatsDto
            {
                TotalMatchesHosted = matches.Count,
                UniqueTeamsHosted = uniqueTeams.Count,
                LastMatchDate = matches.OrderByDescending(m => m.MatchDate).FirstOrDefault()?.MatchDate ?? DateTime.MinValue
            };


        }
    }
}