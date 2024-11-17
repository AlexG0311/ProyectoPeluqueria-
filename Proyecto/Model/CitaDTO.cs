using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Model
{
    public class CitaDTO
    {
        public int idReserva { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string ClienteNombre { get; set; }
        public string ClienteApellido { get; set; }
        public int Estado_idEstado { get; set; }
        public string EstadoDescripcion { get; set; }

        // Propiedad calculada para combinar Fecha y Hora
        public DateTime FechaHora => Fecha.Add(Hora);

        // Propiedad calculada para el nombre completo del cliente
        public string NombreCompleto => $"{ClienteNombre} {ClienteApellido}";
    }
}
