using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IPLAwardManagementSystem.Data;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Services;

namespace IPLAwardManagementSystem.Controllers
{
    public class VoteController : Controller
    {
        private readonly IVoteService _voteService;
        private readonly ApplicationDbContext _context;

        public VoteController(IVoteService voteService, ApplicationDbContext context)
        {
            _voteService = voteService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var votes = await _voteService.GetAllVotesAsync();
            return View(votes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var vote = await _voteService.GetVoteByIdAsync(id);
            if (vote == null) return NotFound();
            return View(vote);
        }

        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VoteCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _voteService.CreateVoteAsync(dto);
                return RedirectToAction(nameof(Index));
            }

            await LoadDropdownsAsync();
            return View(dto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vote = await _context.Votes
                .Include(v => v.Player)
                .Include(v => v.Award)
                .Include(v => v.Voter)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vote == null) return NotFound();

            var dto = new VoteUpdateDto
            {
                Id = vote.Id,
                PlayerId = vote.PlayerId,
                AwardId = vote.AwardId,
                VoterId = vote.VoterId,
                VoteDate = vote.VoteDate
            };

            await LoadDropdownsAsync();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VoteUpdateDto dto)
        {
            if (id != dto.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync();
                return View(dto);
            }

            var vote = await _context.Votes.FindAsync(id);
            if (vote == null) return NotFound();

            vote.PlayerId = dto.PlayerId;
            vote.AwardId = dto.AwardId;
            vote.VoterId = dto.VoterId;
            vote.VoteDate = dto.VoteDate;

            _context.Votes.Update(vote);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var vote = await _voteService.GetVoteByIdAsync(id);
            if (vote == null) return NotFound();
            return View(vote);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _voteService.DeleteVoteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadDropdownsAsync()
        {
            ViewBag.Players = new SelectList(await _context.Players.ToListAsync(), "Id", "Name");
            ViewBag.Awards = new SelectList(await _context.Awards.ToListAsync(), "Id", "Name");
            ViewBag.Voters = new SelectList(await _context.Voters.ToListAsync(), "Id", "Name");
        }
    }
}
