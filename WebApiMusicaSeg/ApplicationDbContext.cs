using Microsoft.EntityFrameworkCore;
using WebApiMusicaSeg.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApiMusicaSeg
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ArtistaCancion>()
                .HasKey(al => new { al.ArtistaId, al.CancionId });
        }

        public DbSet<Artista> Artistas { get; set; }
        public DbSet<Cancion> Canciones { get; set; }

        public DbSet<Albums> Albums { get; set; }

        public DbSet<ArtistaCancion> ArtistaCancion { get; set; }

    }
}
