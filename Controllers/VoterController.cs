using Microsoft.AspNetCore.Mvc;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Services;

namespace IPLAwardManagementSystem.Controllers
{
    public class VoterController : Controller
    {
        private readonly IVoterService _voterService;

        public VoterController(IVoterService voterService)
        {
            _voterService = voterService;
        }

        public async Task<IActionResult> Index()
        {
            var voters = await _voterService.GetAllVotersAsync();
            return View(voters);
        }

        public async Task<IActionResult> Details(int id)
        {
            var voter = await _voterService.GetVoterByIdAsync(id);
            if (voter == null) return NotFound();
            return View(voter);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VoterCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _voterService.CreateVoterAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var voter = await _voterService.GetVoterByIdAsync(id);
            if (voter == null) return NotFound();

            var editDto = new VoterUpdateDto
            {
                Id = voter.Id,
                Name = voter.Name,
                Email = voter.Email,
                Role = voter.Role
            };

            return View(editDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VoterUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _voterService.UpdateVoterAsync(id, dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var voter = await _voterService.GetVoterByIdAsync(id);
            if (voter == null) return NotFound();
            return View(voter);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _voterService.DeleteVoterAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}