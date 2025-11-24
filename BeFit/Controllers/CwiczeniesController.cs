using BeFit.Data;
using BeFit.DTOs;
using BeFit.Models;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BeFit.Controllers
{
    public class CwiczeniesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CwiczeniesController(ApplicationDbContext context)
        {
            _context = context;
        }
        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }

        // GET: Cwiczenies
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Cwiczenies.Include(c => c.SesjaTreningowa);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Cwiczenies/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cwiczenie = await _context.Cwiczenies
                .Include(c => c.SesjaTreningowa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cwiczenie == null)
            {
                return NotFound();
            }

            return View(cwiczenie);
        }

        // GET: Cwiczenies/Create
        
        public IActionResult Create()
        {
            ViewData["SesjaTreningowaId"] = new SelectList(_context.SesjaTreningowas, "Id", "Id");
            return View();
        }

        // POST: Cwiczenies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Create(CwiczenieDTOs dto)
        {
            if (ModelState.IsValid)
            {
                var userId = GetUserId();

                var sesja = await _context.SesjaTreningowas
                    .FirstOrDefaultAsync(s => s.Id == dto.SesjaTreningowaId && s.AppUserId == userId);

                if (sesja == null)
                {
                    return Unauthorized();
                }

                var cwiczenie = new Cwiczenie
                {
                    Name = dto.Name,
                    SesjaTreningowaId = dto.SesjaTreningowaId
                };

                _context.Cwiczenies.Add(cwiczenie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["SesjaTreningowaId"] = new SelectList(_context.SesjaTreningowas, "Id", "Id", dto.SesjaTreningowaId);
            return View(dto);
        }

        // GET: Cwiczenies/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cwiczenie = await _context.Cwiczenies.FindAsync(id);
            if (cwiczenie == null)
            {
                return NotFound();
            }
            ViewData["SesjaTreningowaId"] = new SelectList(_context.SesjaTreningowas, "Id", "Id", cwiczenie.SesjaTreningowaId);
            return View(cwiczenie);
        }

        // POST: Cwiczenies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SesjaTreningowaId")] Cwiczenie cwiczenie)
        {
            if (id != cwiczenie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cwiczenie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CwiczenieExists(cwiczenie.Id))
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
            ViewData["SesjaTreningowaId"] = new SelectList(_context.SesjaTreningowas, "Id", "Id", cwiczenie.SesjaTreningowaId);
            return View(cwiczenie);
        }

        // GET: Cwiczenies/Delete/5
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cwiczenie = await _context.Cwiczenies
                .Include(c => c.SesjaTreningowa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cwiczenie == null)
            {
                return NotFound();
            }

            return View(cwiczenie);
        }

        // POST: Cwiczenies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cwiczenie = await _context.Cwiczenies.FindAsync(id);
            if (cwiczenie != null)
            {
                _context.Cwiczenies.Remove(cwiczenie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CwiczenieExists(int id)
        {
            return _context.Cwiczenies.Any(e => e.Id == id);
        }
    }
}
