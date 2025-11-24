using BeFit.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<SesjaTreningowa> SesjaTreningowas { get; set; }
        public DbSet<Cwiczenie> Cwiczenies { get; set; }
        public DbSet<Podsumowanie> Podsumowanies { get; set; }
    }
}
