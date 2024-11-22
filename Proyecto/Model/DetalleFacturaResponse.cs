using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Model
{
    public class DetalleFacturaResponse
    {

        public int ProductoId { get; set; } // ID del producto comprado
        public string NombreProducto { get; set; } // Nombre del producto
        public int Cantidad { get; set; } // Cantidad comprada
        public decimal Precio { get; set; } // Precio unitario del producto
        public decimal Subtotal => Cantidad * Precio; // Subtotal del producto (calculado)
    }
}
