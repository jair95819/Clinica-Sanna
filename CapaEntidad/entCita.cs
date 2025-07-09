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
        public string nombrePaciente { get; set; }
        public string docPaciente { get; set; }
        public string telefonoPaciente { get; set; }

        // Datos del Médico
        public int idMedico { get; set; }
        public string nombreMedico { get; set; }
        public string especialidadMedico { get; set; }

        // Detalles de la Cita
        public int idCita { get; set; }
        public string fCita { get; set; }
        public string hCita { get; set; }
        public string sedeCita { get; set; }
        public string consCita { get; set; }
    }

}
