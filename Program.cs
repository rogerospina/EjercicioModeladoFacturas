using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EjercicioModeladoFacturas.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EjercicioModeladoFacturas
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreandoLaFacturaYSuDetalleDeManeraSimultanea();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        static void CreandoLaFacturaYSuDetalleDeManeraSimultanea()
        {
            using (var context = new ApplicationDbContext())
            {
                List<Producto> productos = context.Productos.ToList();

                var facturaDetalle1 = new FacturaDetalle();
                facturaDetalle1.ProductoId = productos[0].Id;
                facturaDetalle1.Precio = productos[0].Precio;
                facturaDetalle1.Cantidad = 3;

                var facturaDetalle2 = new FacturaDetalle();
                facturaDetalle2.ProductoId = productos[1].Id;
                facturaDetalle2.Precio = productos[1].Precio;
                facturaDetalle2.Cantidad = 1;

                List<FacturaDetalle> detalles = new List<FacturaDetalle>() { facturaDetalle1, facturaDetalle2 };

                var factura = new Factura();
                factura.FechaEmision = DateTime.Now;
                factura.Detalle = detalles;
                factura.Total = detalles.Sum(x => x.Precio * x.Cantidad);

                context.Add(factura);

                try
                {
                    context.SaveChanges();
                }
                catch(DbUpdateException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
