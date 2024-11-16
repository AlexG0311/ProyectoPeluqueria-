using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Model
{
    public class ClienteDTO
    {
        [Key]
        public int idCliente { get; set; }
    }
}
