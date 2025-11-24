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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SesjaTreningowa>()
                .HasOne(s => s.AppUser)
                .WithMany()
                .HasForeignKey(s => s.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Podsumowanie>()
                .HasOne(p => p.SesjaTreningowa)
                .WithMany(s => s.Podsumowania)
                .HasForeignKey(p => p.SesjaTreningowaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Podsumowanie>()
                .HasOne(p => p.Cwiczenie)
                .WithMany(c => c.Podsumowania)
                .HasForeignKey(p => p.CwiczenieId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Podsumowanie>()
                .HasOne(p => p.AppUser)
                .WithMany()
                .HasForeignKey(p => p.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
