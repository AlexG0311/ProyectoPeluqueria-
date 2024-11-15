using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Model
{
    public class Rolusuario
    {
        [PrimaryKey, AutoIncrement]
        public int Rol_idRol { get; set; }
        public int usuario_idUsuario { get; set; }
        public Rol Rol { get; set; }
        public Usuarios Usuario { get; set; }
    }
}
