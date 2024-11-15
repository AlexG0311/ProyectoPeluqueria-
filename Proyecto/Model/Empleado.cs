using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLite;

namespace Proyecto.Model
{
    public class Empleado
    {
        [PrimaryKey, AutoIncrement]
        [JsonProperty("idEmpleado")]
        public int idEmpleado { get; set; }

        [JsonProperty("Usuario_idUsuario")]
        public int Usuario_idUsuario { get; set; }
    }
}
