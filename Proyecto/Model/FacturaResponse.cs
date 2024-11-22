using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Model
{
    public class FacturaResponse
    {

        public int IdFactura { get; set; } // ID único de la factura generada
        public DateTime FechaFactura { get; set; } // Fecha en la que se generó la factura
        public decimal Total { get; set; } // Monto total de la factura
        public List<DetalleFacturaResponse> Detalles { get; set; } // Lista de productos incluidos en la factura
    }
}
