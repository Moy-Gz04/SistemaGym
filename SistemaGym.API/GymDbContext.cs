using Microsoft.EntityFrameworkCore;
using SistemaGym.API.Models;

namespace SistemaGym.API.Data
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options)
            : base(options)
        {
        }

        public DbSet<Miembro> Miembros { get; set; }
        public DbSet<Acceso> Accesos { get; set; }
        public DbSet<Pago> Pagos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pago>()
                .Property(p => p.Monto)
                .HasPrecision(10, 2);
        }
    }
}