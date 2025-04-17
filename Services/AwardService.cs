using AutoMapper;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Models;
using IPLAwardManagementSystem.Services;
using IPLAwardManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace IPLAwardManagementSystem.Services
{
    public class AwardService : IAwardService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AwardService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AwardDto> CreateAwardAsync(AwardCreateDto awardDto)
        {
            var award = _mapper.Map<Award>(awardDto);
            _context.Awards.Add(award);
            await _context.SaveChangesAsync();
            return _mapper.Map<AwardDto>(award);
        }

        public async Task DeleteAwardAsync(int id)
        {
            var award = await _context.Awards.FindAsync(id);
            if (award != null)
            {
                _context.Awards.Remove(award);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<AwardDto>> GetAllAwardsAsync()
        {
            var awards = await _context.Awards.ToListAsync();
            return _mapper.Map<List<AwardDto>>(awards);
        }

        public async Task<AwardDto> GetAwardByIdAsync(int id)
        {
            var award = await _context.Awards.FindAsync(id);
            return award == null ? null : _mapper.Map<AwardDto>(award);
        }

        public async Task UpdateAwardAsync(int id, AwardUpdateDto awardDto)
        {
            var award = await _context.Awards.FindAsync(id);
            if (award != null)
            {
                _mapper.Map(awardDto, award);
                await _context.SaveChangesAsync();
            }
        }

        public async Task NominatePlayerAsync(int awardId, int playerId)
        {
            if (!await IsPlayerNominatedAsync(awardId, playerId))
            {
                var votes = await _context.Votes.CountAsync(v => v.AwardId == awardId && v.PlayerId == playerId);

                var nomination = new PlayerAward
                {
                    AwardId = awardId,
                    PlayerId = playerId,
                    NominationDate = DateTime.UtcNow,
                    IsWinner = false,
                    VotesReceived = votes
                };

                _context.PlayerAwards.Add(nomination);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveNominationAsync(int awardId, int playerId)
        {
            var nomination = await _context.PlayerAwards.FindAsync(playerId, awardId);
            if (nomination != null)
            {
                _context.PlayerAwards.Remove(nomination);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeclareWinnerAsync(int awardId, int playerId)
        {
            var nomination = await _context.PlayerAwards.FindAsync(playerId, awardId);
            if (nomination != null)
            {
                nomination.IsWinner = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task RevokeWinnerStatusAsync(int awardId, int playerId)
        {
            var nomination = await _context.PlayerAwards.FindAsync(playerId, awardId);
            if (nomination != null)
            {
                nomination.IsWinner = false;
                await _context.SaveChangesAsync();
            }
        }

        // ✅ LIVE VOTE COUNT + WINNER CALCULATION
        public async Task<IEnumerable<PlayerAwardDto>> GetAwardNomineesAsync(int awardId)
        {
            var nominations = await _context.PlayerAwards
                .Include(pa => pa.Player)
                .Include(pa => pa.Award)
                .Where(pa => pa.AwardId == awardId)
                .ToListAsync();

            var votes = await _context.Votes
                .Where(v => v.AwardId == awardId)
                .GroupBy(v => v.PlayerId)
                .Select(g => new { PlayerId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.PlayerId, g => g.Count);

            var topVote = votes.OrderByDescending(x => x.Value).FirstOrDefault();
            var topPlayerId = topVote.Key;
            var maxVotes = topVote.Value;

            var results = nominations.Select(n => new PlayerAwardDto
            {
                PlayerId = n.PlayerId,
                PlayerName = n.Player?.Name ?? "Unknown",
                AwardId = n.AwardId,
                AwardName = n.Award?.Title ?? "Unknown",
                NominationDate = n.NominationDate,
                VotesReceived = votes.ContainsKey(n.PlayerId) ? votes[n.PlayerId] : 0,
                IsWinner = n.PlayerId == topPlayerId && maxVotes > 0
            }).ToList();

            return results;
        }

        public async Task<IEnumerable<PlayerAwardDto>> GetAwardWinnersAsync(int awardId)
        {
            var winners = await _context.PlayerAwards
                .Include(pa => pa.Player)
                .Include(pa => pa.Award)
                .Where(pa => pa.AwardId == awardId && pa.IsWinner)
                .ToListAsync();

            return _mapper.Map<List<PlayerAwardDto>>(winners);
        }

        public async Task<int> GetVoteCountAsync(int awardId, int playerId)
        {
            return await _context.Votes.CountAsync(v => v.AwardId == awardId && v.PlayerId == playerId);
        }

        public async Task<bool> IsPlayerNominatedAsync(int awardId, int playerId)
        {
            return await _context.PlayerAwards.AnyAsync(pa => pa.AwardId == awardId && pa.PlayerId == playerId);
        }
    }
}
