using Microsoft.AspNetCore.Mvc;
using IPLAwardManagementSystem.DTOs;
using IPLAwardManagementSystem.Services;

namespace IPLAwardManagementSystem.Controllers
{
    public class AwardController : Controller
    {
        private readonly IAwardService _awardService;

        public AwardController(IAwardService awardService)
        {
            _awardService = awardService;
        }

        public async Task<IActionResult> Index()
        {
            var awards = await _awardService.GetAllAwardsAsync();
            return View(awards);
        }

        public async Task<IActionResult> Details(int id)
        {
            var award = await _awardService.GetAwardByIdAsync(id);
            if (award == null) return NotFound();
            return View(award);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(AwardCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _awardService.CreateAwardAsync(dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var award = await _awardService.GetAwardByIdAsync(id);
            if (award == null) return NotFound();
            return View(new AwardUpdateDto { Name = award.Name, Description = award.Description, IsActive = award.IsActive });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AwardUpdateDto dto)
        {
            if (ModelState.IsValid)
            {
                await _awardService.UpdateAwardAsync(id, dto);
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var award = await _awardService.GetAwardByIdAsync(id);
            if (award == null) return NotFound();
            return View(award);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _awardService.DeleteAwardAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}