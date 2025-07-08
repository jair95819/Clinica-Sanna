using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entUsuario
    {
        public int idUsuario { get; set; }
        public string usuario { get; set; }
        public string contraseña { get; set; }
        public string tipoUsuario { get; set; } // Cliente o Admin
        public bool estado { get; set; }
    }
}
