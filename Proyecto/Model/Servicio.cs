using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Model
{
  public class Servicio
    {
        [Key]
        public int IdServicio { get; set; }
        public string Nombre { get; set; }
        public string Img { get; set; }
        public string Calificacion { get; set; }
    }
}
