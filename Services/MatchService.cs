using AutoMapper;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Models;
using IPLAwardManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace IPLAwardManagementSystem.Services
{
    public class MatchService : IMatchService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MatchService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MatchDto> CreateMatchAsync(MatchCreateDto dto)
        {
            var match = _mapper.Map<Match>(dto);
            _context.Matches.Add(match);
            await _context.SaveChangesAsync();
            return _mapper.Map<MatchDto>(match);
        }

        public async Task DeleteMatchAsync(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match != null)
            {
                _context.Matches.Remove(match);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<MatchDto>> GetAllMatchesAsync()
        {
            var matches = await _context.Matches
                .Include(m => m.Venue)
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .ToListAsync();
            return _mapper.Map<List<MatchDto>>(matches);
        }

        public async Task<MatchDto?> GetMatchByIdAsync(int id)
        {
            var match = await _context.Matches
                .Include(m => m.Venue)
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .FirstOrDefaultAsync(m => m.MatchId == id);
            return match == null ? null : _mapper.Map<MatchDto>(match);
        }

        public async Task UpdateMatchAsync(int id, MatchUpdateDto dto)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match != null)
            {
                _mapper.Map(dto, match);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<MatchDto>> GetMatchesByVenueAsync(int venueId)
        {
            var matches = await _context.Matches
                .Where(m => m.VenueId == venueId)
                .Include(m => m.Venue)
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .ToListAsync();
            return _mapper.Map<List<MatchDto>>(matches);
        }

        public async Task<IEnumerable<MatchDto>> GetMatchesByTeamAsync(int teamId)
        {
            var matches = await _context.Matches
                .Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId)
                .Include(m => m.Venue)
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .ToListAsync();
            return _mapper.Map<List<MatchDto>>(matches);
        }
    }
}