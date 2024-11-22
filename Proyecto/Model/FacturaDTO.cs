using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Model
{
    public class FacturaDTO
    {

        public DateTime FechaFactura { get; set; } // Fecha de la factura
        public decimal Total { get; set; } // Total del carrito
        public List<DetalleFacturaDTO> Detalles { get; set; } // Lista de detalles de la factura

    }
}
