using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Proyecto.Model
{
    public class Usuarios
    {
        [PrimaryKey, AutoIncrement]
        [JsonProperty("idusuario")]
        public int idUsuario { get; set; }

        [JsonProperty("nombre")]
        [Required]
        [StringLength(45)]
        public string nombre { get; set; }

        [JsonProperty("apellidos")]
        [Required]
        [StringLength(45)]
        public string apellidos { get; set; }

        [JsonProperty("correo")]
        [Required]
        [StringLength(45)]
        [EmailAddress]
        public string correo { get; set; }

        [JsonProperty("contrasena")]
        [Required]
        [StringLength(45)]
        public string contrasena { get; set; }

        [JsonProperty("telefono")]
        [Required]
        [StringLength(15)]
        public string telefono { get; set; }

        // Agregar la propiedad RolesUsuario como lista de Rolusuario
        public List<Rolusuario> RolesUsuario { get; set; } = new List<Rolusuario>();

    }
}
