using IPLAwardManagementSystem.Data;
using AutoMapper;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Interfaces;
using IPLAwardManagementSystem.Models;
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

        public async Task<MatchDto> CreateMatchAsync(MatchCreateDto matchCreateDto)
        {
            var match = _mapper.Map<Match>(matchCreateDto);

            var teams = await _context.Teams
                .Where(t => matchCreateDto.TeamIds.Contains(t.TeamId))
                .ToListAsync();

            foreach (var team in teams)
            {
                match.Teams.Add(team);
            }

            _context.Matches.Add(match);
            await _context.SaveChangesAsync();
            return _mapper.Map<MatchDto>(match);
        }

        public async Task<IEnumerable<MatchDto>> GetAllMatchesAsync()
        {
            var matches = await _context.Matches
                .Include(m => m.Venue)
                .Include(m => m.Teams)
                .ToListAsync();

            return _mapper.Map<IEnumerable<MatchDto>>(matches);
        }

        public async Task<MatchDto> GetMatchByIdAsync(int id)
        {
            var match = await _context.Matches
                .Include(m => m.Venue)
                .Include(m => m.Teams)
                .FirstOrDefaultAsync(m => m.MatchId == id);

            if (match == null) throw new KeyNotFoundException("Match not found");
            return _mapper.Map<MatchDto>(match);
        }

        public async Task UpdateMatchAsync(int id, MatchUpdateDto matchUpdateDto)
        {
            var match = await _context.Matches
                .Include(m => m.Teams)
                .FirstOrDefaultAsync(m => m.MatchId == id);

            if (match == null) throw new KeyNotFoundException("Match not found");

            _mapper.Map(matchUpdateDto, match);

            // Update teams
            match.Teams.Clear();
            var teams = await _context.Teams
                .Where(t => matchUpdateDto.TeamIds.Contains(t.TeamId))
                .ToListAsync();

            foreach (var team in teams)
            {
                match.Teams.Add(team);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteMatchAsync(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null) throw new KeyNotFoundException("Match not found");

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MatchDto>> GetMatchesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var matches = await _context.Matches
                .Where(m => m.MatchDate >= startDate && m.MatchDate <= endDate)
                .Include(m => m.Venue)
                .Include(m => m.Teams)
                .ToListAsync();

            return _mapper.Map<IEnumerable<MatchDto>>(matches);
        }
    }
}