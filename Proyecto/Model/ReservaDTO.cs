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
        public DateTime Fecha { get; set; } // Fecha de la reserva
        public TimeSpan Hora { get; set; } // Hora de inicio de la reserva
        public int Cliente_idCliente { get; set; } // ID del cliente que realiza la reserva
        public int Servicio_idServicio { get; set; } // ID del servicio seleccionado
        public int Empleado_idEmpleado { get; set; } // ID del empleado asignado a la reserva
        
        public int Estado_idEstado { get; set; } // ID del estado de la reserva (Pendiente, Confirmada, etc.)
        public string Estado { get; set; }
       
    }

}
