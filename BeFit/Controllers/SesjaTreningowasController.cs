using BeFit.Data;
using BeFit.Models;
using BeFit.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BeFit.Controllers
{
    [Authorize]
    public class SesjaTreningowasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }

        public SesjaTreningowasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SesjaTreningowas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SesjaTreningowas.Include(s => s.AppUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SesjaTreningowas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesjaTreningowa = await _context.SesjaTreningowas
                .Include(s => s.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sesjaTreningowa == null)
            {
                return NotFound();
            }

            return View(sesjaTreningowa);
        }

        // GET: SesjaTreningowas/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: SesjaTreningowas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SesjaTreningowaDTOs dto)
        {
            if (ModelState.IsValid)
            {
                var userId = GetUserId();

                var sesja = new SesjaTreningowa
                {
                    StartTime = dto.StartTime,
                    EndTime = dto.EndTime,
                    AppUserId = userId
                };

                _context.SesjaTreningowas.Add(sesja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        // GET: SesjaTreningowas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesjaTreningowa = await _context.SesjaTreningowas.FindAsync(id);
            if (sesjaTreningowa == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", sesjaTreningowa.AppUserId);
            return View(sesjaTreningowa);
        }

        // POST: SesjaTreningowas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartTime,EndTime,AppUserId")] SesjaTreningowa sesjaTreningowa)
        {
            if (id != sesjaTreningowa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sesjaTreningowa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SesjaTreningowaExists(sesjaTreningowa.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", sesjaTreningowa.AppUserId);
            return View(sesjaTreningowa);
        }

        // GET: SesjaTreningowas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sesjaTreningowa = await _context.SesjaTreningowas
                .Include(s => s.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sesjaTreningowa == null)
            {
                return NotFound();
            }

            return View(sesjaTreningowa);
        }

        // POST: SesjaTreningowas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sesjaTreningowa = await _context.SesjaTreningowas.FindAsync(id);
            if (sesjaTreningowa != null)
            {
                _context.SesjaTreningowas.Remove(sesjaTreningowa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SesjaTreningowaExists(int id)
        {
            return _context.SesjaTreningowas.Any(e => e.Id == id);
        }
    }
}
