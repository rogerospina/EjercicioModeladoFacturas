using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EjercicioModeladoFacturas.Models
{
    public class FacturaDetalle
    {
        public int Id { get; set; }

        [Required]
        public int FacturaId { get; set; }

        public Factura Factura { get; set; }

        [Required]
        public int ProductoId { get; set; }

        public Producto Producto { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [Required]
        public int Cantidad { get; set; }

        public decimal Total { get; set; }
    }
}
