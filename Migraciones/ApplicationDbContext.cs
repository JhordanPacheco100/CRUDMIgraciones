using Microsoft.EntityFrameworkCore;
using Migraciones.Models;

namespace Migraciones
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Empleado> Empleados { get; set; }
    }
}
