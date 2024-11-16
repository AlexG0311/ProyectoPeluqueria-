using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Model
{
    public interface IUsuarioDTO
    {
        int idUsuario { get; set; }
        string Nombre { get; set; }
        string Apellidos { get; set; }
        string Telefono { get; set; }
    }
}
