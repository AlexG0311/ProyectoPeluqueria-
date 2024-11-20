using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Model
{
    public class ReseñaProductoDTO
    {
        public int Estrellas { get; set; }
        public string Comentario { get; set; }
        public int Cliente_idCliente { get; set; }
        public int Producto_idProducto { get; set; }
    }
}
