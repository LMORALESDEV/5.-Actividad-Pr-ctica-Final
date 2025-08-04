using AgenciaDeTours.Models;
using Microsoft.EntityFrameworkCore;

namespace AgenciaDeTours.Datos
{
    public class AgenciaDeToursDbContext : DbContext
    {
        public AgenciaDeToursDbContext(DbContextOptions options) : base(options)
        {
        }

        protected AgenciaDeToursDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TourViewModel>()
                   .Property(e => e.EstadoTour)
                   .HasComputedColumnSql(
                       "CASE WHEN [Fecha] > CAST(GETDATE() AS date) THEN 1 ELSE 2 END",
                       stored: false 
                   );

        }

        public DbSet<PaisViewModel> Paises { get; set; }
        public DbSet<DestinoViewModel> Destinos { get; set; }
        public DbSet<TourViewModel> Tours { get; set; }
    }
}
