using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Model
{
    public class EmpleadoDTOO : IUsuarioDTO
    {
        public int idUsuario { get; set; } // ID del usuario en la tabla Usuarios
        public int idEmpleado { get; set; } // ID del empleado específico
        public string Nombre { get; set; } // Nombre del empleado
        public string Apellidos { get; set; } // Apellidos del empleado
        public string Correo { get; set; } // Correo electrónico del empleado
        public string Telefono { get; set; } // Teléfono del empleado
    }
}
