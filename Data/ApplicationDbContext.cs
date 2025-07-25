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
        public DbSet<Tamal> Tamales { get; set; }
        public DbSet<Bebida> Bebidas { get; set; }
        public DbSet<Catalogo> Catalogos { get; set; }
        public DbSet<CatalogoItem> CatalogoItems { get; set; }
        // public DbSet<Combo> Combos { get; set; }
        // public DbSet<Venta> Ventas { get; set; }
        // public DbSet<DetalleVenta> DetalleVentas { get; set; }
        // public DbSet<Inventario> Inventarios { get; set; }

        // Desactivar las migraciones automáticas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Puedes agregar más configuraciones si es necesario para las demás entidades.
            // Especificar el esquema "cazuela_chapina" para la tabla Tamales
            modelBuilder.Entity<Tamal>().ToTable("tamales", "cazuela_chapina");
            modelBuilder.Entity<Bebida>().ToTable("bebidas", "cazuela_chapina");
            modelBuilder.Entity<Usuario>().ToTable("usuarios", "cazuela_chapina");
            modelBuilder.Entity<Rol>().ToTable("roles", "cazuela_chapina");
            modelBuilder.Entity<Catalogo>().ToTable("catalogo", "cazuela_chapina");
            modelBuilder.Entity<CatalogoItem>().ToTable("catalogo_item", "cazuela_chapina");

            // Relación entre Venta y DetalleVenta (una venta tiene varios detalles)
            // modelBuilder.Entity<Venta>()
            //     .HasMany(v => v.DetalleVentas)
            //     .WithOne(d => d.Venta)
            //     .HasForeignKey(d => d.IdVentaFk);
            //
            // // Relación entre Producto (Tamal) y DetalleVenta (un detalle se refiere a un producto)
            // modelBuilder.Entity<DetalleVenta>()
            //     .HasOne(d => d.Producto)
            //     .WithMany()
            //     .HasForeignKey(d => d.IdProductoFk);
            //
            // // Relación entre Inventario y Producto (un inventario tiene un producto específico)
            // modelBuilder.Entity<Inventario>()
            //     .HasOne(i => i.Producto)
            //     .WithMany()
            //     .HasForeignKey(i => i.IdProductoFk);
            //
            // // Relación entre CatalogoItem y Catalogo (un item pertenece a un catálogo)
            modelBuilder.Entity<Catalogo>()
              .HasMany(c => c.CatalogoItems)
              .WithOne()
              .HasForeignKey(ci => ci.IdCatalogo) // Este es el campo de relación
              .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<CatalogoItem>()
              .HasOne(ci => ci.Catalogo)
              .WithMany(c => c.CatalogoItems) // necesitas una colección en Catalogo
              .HasForeignKey(ci => ci.IdCatalogo) // la columna real en catalogo_item
              .OnDelete(DeleteBehavior.Restrict);

            /*====================================*/
            /*             BEBIDA                 */
            /*====================================*/
            modelBuilder.Entity<Bebida>()
              .HasOne(b => b.TipoBebida)
              .WithMany()
              .HasForeignKey(b => b.IdTipoBebida)
              .OnDelete(DeleteBehavior.Restrict);

            // Relación entre Bebida y CatalogoItem para Tamano
            modelBuilder.Entity<Bebida>()
              .HasOne(b => b.Tamano)
              .WithMany()
              .HasForeignKey(b => b.IdTamanoFk)
              .OnDelete(DeleteBehavior.Restrict);

            // Relación entre Bebida y CatalogoItem para Endulzante
            modelBuilder.Entity<Bebida>()
              .HasOne(b => b.Endulzante)
              .WithMany()
              .HasForeignKey(b => b.IdEndulzanteFk)
              .OnDelete(DeleteBehavior.Restrict);

            // Relación entre Bebida y CatalogoItem para Topping
            modelBuilder.Entity<Bebida>()
              .HasOne(b => b.Topping)
              .WithMany()
              .HasForeignKey(b => b.IdToppingFk)
              .OnDelete(DeleteBehavior.Restrict);


            /*====================================*/
            /*             TAMAL                  */
            /*====================================*/
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
              // Tablas
              entity.SetTableName(ToSnakeCase(entity.GetTableName()));

              // Columnas
              foreach (var property in entity.GetProperties())
              {
                property.SetColumnName(ToSnakeCase(property.Name));
              }

              // Claves
              foreach (var key in entity.GetKeys())
              {
                key.SetName(ToSnakeCase(key.GetName()));
              }

              // Índices
              foreach (var index in entity.GetIndexes())
              {
                index.SetDatabaseName(ToSnakeCase(index.GetDatabaseName()));
              }
            }

            // Configuración de relaciones
            modelBuilder.Entity<Tamal>()
              .HasOne(t => t.TipoMasa)
              .WithMany()
              .HasForeignKey(t => t.IdTipoMasaFk)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tamal>()
              .HasOne(t => t.Relleno)
              .WithMany()
              .HasForeignKey(t => t.IdRellenoFk)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tamal>()
              .HasOne(t => t.Envoltura)
              .WithMany()
              .HasForeignKey(t => t.IdEnvolturaFk)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tamal>()
              .HasOne(t => t.NivelPicante)
              .WithMany()
              .HasForeignKey(t => t.IdNivelPicante)
              .OnDelete(DeleteBehavior.Restrict);
        }
        private string ToSnakeCase(string name) =>
          string.Concat(name.Select((x, i) =>
                i > 0 && char.IsUpper(x) ? "_" + x : x.ToString())).ToLower();
    }
}
