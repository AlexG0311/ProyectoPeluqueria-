using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SQLite;
namespace Proyecto.Model
{
    public class Cliente
    {

        [PrimaryKey, AutoIncrement]
        [JsonProperty("idCliente")]
        public int idCliente { get; set; }

        [JsonProperty("Usuario_idUsuario")]
        public int Usuario_idUsuario { get; set; }
    }
}
