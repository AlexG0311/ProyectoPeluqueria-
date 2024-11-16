using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Model
{
    public class LoginResponseDto
    {
        public int idUsuario { get; set; }
        public string TipoUsuario { get; set; }
        public int? idCliente { get; set; }
        public int? idEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
    }
}
