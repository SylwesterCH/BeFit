using BeFit.Data;
using BeFit.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BeFit.Controllers
{
    public class PodsumowaniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PodsumowaniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }

        // GET: Podsumowanies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Podsumowanies.Include(p => p.Cwiczenie).Include(p => p.Sesja);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Podsumowanies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podsumowanie = await _context.Podsumowanies
                .Include(p => p.Cwiczenie)
                .Include(p => p.Sesja)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (podsumowanie == null)
            {
                return NotFound();
            }

            return View(podsumowanie);
        }

        // GET: Podsumowanies/Create
        public IActionResult Create()
        {
            ViewData["CwiczenieId"] = new SelectList(_context.Cwiczenies, "Id", "Name");
            ViewData["SesjaId"] = new SelectList(_context.SesjaTreningowas, "Id", "StartTime");
            return View();
        }

        // POST: Podsumowanies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SesjaId,CwiczenieId,Obciazenie,Serie,Powtorzenia")] Podsumowanie podsumowanie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(podsumowanie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CwiczenieId"] = new SelectList(_context.Cwiczenies, "Id", "Name", podsumowanie.CwiczenieId);
            ViewData["SesjaId"] = new SelectList(_context.SesjaTreningowas, "Id", "StartTime", podsumowanie.SesjaId);
            return View(podsumowanie);
        }

        // GET: Podsumowanies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podsumowanie = await _context.Podsumowanies.FindAsync(id);
            if (podsumowanie == null)
            {
                return NotFound();
            }
            ViewData["CwiczenieId"] = new SelectList(_context.Cwiczenies, "Id", "Name", podsumowanie.CwiczenieId);
            ViewData["SesjaId"] = new SelectList(_context.SesjaTreningowas, "Id", "StartTime", podsumowanie.SesjaId);
            return View(podsumowanie);
        }

        // POST: Podsumowanies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SesjaId,CwiczenieId,Obciazenie,Serie,Powtorzenia")] Podsumowanie podsumowanie)
        {
            if (id != podsumowanie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(podsumowanie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PodsumowanieExists(podsumowanie.Id))
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
            ViewData["CwiczenieId"] = new SelectList(_context.Cwiczenies, "Id", "Name", podsumowanie.CwiczenieId);
            ViewData["SesjaId"] = new SelectList(_context.SesjaTreningowas, "Id", "StartTime", podsumowanie.SesjaId);
            return View(podsumowanie);
        }

        // GET: Podsumowanies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podsumowanie = await _context.Podsumowanies
                .Include(p => p.Cwiczenie)
                .Include(p => p.Sesja)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (podsumowanie == null)
            {
                return NotFound();
            }

            return View(podsumowanie);
        }

        // POST: Podsumowanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var podsumowanie = await _context.Podsumowanies.FindAsync(id);
            if (podsumowanie != null)
            {
                _context.Podsumowanies.Remove(podsumowanie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PodsumowanieExists(int id)
        {
            return _context.Podsumowanies.Any(e => e.Id == id);
        }
    }
}
