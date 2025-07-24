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

        public DbSet<PaisViewModel> Paises { get; set; }
    }
}
