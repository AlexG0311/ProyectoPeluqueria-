using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Model
{
    public class ReservaDTO
    {
        public int idReserva { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public int Cliente_idCliente { get; set; }
        public int Servicio_idServicio { get; set; }
        public int Asignacion_idAsignacion { get; set; }
        public int Estado_idEstado { get; set; }
    }

}
