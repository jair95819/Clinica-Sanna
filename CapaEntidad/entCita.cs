using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entCita
    {
        // Datos del Paciente
        public int PacienteID { get; set; }

        // Datos del Médico
        public int idMedico { get; set; }
        public int? idEspecialidad { get; set; }

        // Detalles de la Cita
        public int? idCita { get; set; }
        public DateTime fCita { get; set; }
        public int tCita { get; set; }
        public int sedeCita { get; set; }
        public string consCita { get; set; }
        public string estCita { get; set; }
    }

}
