using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entPaciente
    {
        public int PacienteID { get; set; }           // PK
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NumDoc { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Sexo { get; set; }
        public bool Estado { get; set; }

        public int UsuarioID { get; set; }            // FK a Usuario
        public entUsuario Usuario { get; set; }       // navegación
    }

}
