using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EjercicioModeladoFacturas.Models
{
    public class Factura
    {
        public int Id { get; set; }

        [Required]
        public DateTime FechaEmision { get; set; }

        public decimal Total { get; set; }

        public List<FacturaDetalle> Detalle { get; set; }
    }
}
