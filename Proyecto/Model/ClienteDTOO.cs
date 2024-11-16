using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Model
{
    public class ClienteDTOO : IUsuarioDTO
    {
        public int idUsuario { get; set; }
        public int idCliente { get; set; } // Asegúrate de que esta propiedad exista
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; } // Correo electrónico del empleado
        public string Telefono { get; set; } // Teléfono del empleado
    }
}
