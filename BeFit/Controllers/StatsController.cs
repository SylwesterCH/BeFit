using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using BeFit.Data;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace BeFit.Controllers
{
    [Authorize]
    public class StatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //GET: Stats
        public async Task<IActionResult> Index()
        {
            var fourWeeksAgo = DateTime.Now.AddDays(-28);

            var stats = await _context.Podsumowanies
                .Include(p => p.Cwiczenie)
                .Include(p => p.Sesja)
                .Where(p => p.Sesja.StartTime >= fourWeeksAgo)
                .GroupBy(p => p.Cwiczenie.Name)
                .Select(g => new
                {
                    Cwiczenie = g.Key,
                    WykonaneRazy = g.Count(),
                    LacznePowtorzenia = g.Sum(x => x.Serie * x.Powtorzenia),
                    SrednieObciazenie = g.Average(x => x.Obciazenie),
                    MaksymalneObciazenie = g.Max(x => x.Obciazenie)
                })
                .ToListAsync();

            return View(stats);
        }
    }
}
