using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EjercicioModeladoFacturas.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public ApplicationDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=ModeladoFacturas;Integrated Security=True")
               // Esta opción solo debe ser usada en tiempo de desarrollo
               .EnableSensitiveDataLogging(true)
               .UseLoggerFactory(GetLoggerFactory());
        }

        // El uso del Logger Factory es nuevo!
        private ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
                   builder.AddConsole()
                          .AddFilter(DbLoggerCategory.Database.Command.Name,
                                     LogLevel.Information));
            return serviceCollection.BuildServiceProvider()
                    .GetService<ILoggerFactory>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Producto> productos = new List<Producto>();
            productos.Add(new Producto() { Id = 21, Nombre = "Lámpara", Descripcion = "Para iluminar tu vida", Precio = 40.99m });
            productos.Add(new Producto() { Id = 22, Nombre = "Tableta Inteligente", Descripcion = "Para que sus hijos se críen solos", Precio = 180.99m });

            modelBuilder.Entity<Producto>().HasData(productos);

            modelBuilder.Entity<FacturaDetalle>().Property(x => x.Total).HasComputedColumnSql("[Cantidad] * [Precio]");

            modelBuilder.Entity<Producto>().Property(x => x.Precio).HasColumnType("decimal(9,2)");
            modelBuilder.Entity<FacturaDetalle>().Property(x => x.Precio).HasColumnType("decimal(9,2)");
            modelBuilder.Entity<FacturaDetalle>().Property(x => x.Total).HasColumnType("decimal(12,2)");
            modelBuilder.Entity<Factura>().Property(x => x.Total).HasColumnType("decimal(16,2)");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Factura> Facturas { get; set; }
        public DbSet<FacturaDetalle> FacturaDetalles { get; set; }
        public DbSet<Producto> Productos { get; set; }
    }
}
