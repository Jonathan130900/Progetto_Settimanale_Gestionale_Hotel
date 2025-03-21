using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Progetto_Settimanale_Gestionale_Hotel.Models;

namespace Progetto_Settimanale_Gestionale_Hotel.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {}
        public DbSet<Cliente> Clienti { get; set; }
        public DbSet<Camera> Camere { get; set; }
        public DbSet<Prenotazione> Prenotazioni { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Prenotazione>()
                .HasOne(p => p.Cliente)
                .WithMany(c => c.Prenotazioni)
                .HasForeignKey(p => p.ClienteId);

            modelBuilder.Entity<Prenotazione>()
                .HasOne(p => p.Camera)
                .WithMany(c => c.Prenotazioni)
                .HasForeignKey(p => p.CameraId);

            modelBuilder.Entity<Camera>()
                .Property(c => c.Prezzo)
                .HasPrecision(10, 2);
        }
    }

}
