using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entUsuario
    {
        public int UsuarioID { get; set; }              // PK
        public string Email { get; set; }
        public string Password { get; set; }

        public int TipousuarioID { get; set; }          // FK a Tipousuario
        public entTipousuario TipoUsuario { get; set; } // navegación
    }

}
