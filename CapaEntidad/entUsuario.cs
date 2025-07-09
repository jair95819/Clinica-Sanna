using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace CapaEntidad
{
    public class entUsuario
    {
        public int UsuarioID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }  // Asegúrate de usar hash y no almacenar contraseñas en texto claro
        public string TipoUsuario { get; set; }  // "Paciente", "Medico", "Administrador"
    }
}
