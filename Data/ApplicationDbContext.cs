using Microsoft.EntityFrameworkCore;
using LaCazuelaChapinaAPI.Models;

namespace LaCazuelaChapinaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        // DbSets representando las tablas existentes
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        // public DbSet<Tamales> Tamales { get; set; }
        // public DbSet<Bebida> Bebidas { get; set; }
        // public DbSet<Combo> Combos { get; set; }
        // public DbSet<Venta> Ventas { get; set; }
        // public DbSet<DetalleVenta> DetalleVentas { get; set; }
        // public DbSet<Inventario> Inventarios { get; set; }

        // Desactivar las migraciones automáticas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Puedes agregar más configuraciones si es necesario para las demás entidades.
        }
    }
}
