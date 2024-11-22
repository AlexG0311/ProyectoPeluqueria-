using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Model
{
    public class DetalleFacturaDTO
    {

        public int ProductoId { get; set; } // ID del producto
        public int Cantidad { get; set; } // Cantidad comprada
        public decimal Precio { get; set; } // Precio unitario del producto
    }
}
