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
        public int idPaciente { get; set; }
        public string rucPaciente { get; set; }
        public string razonSocial { get; set; }
        public string dirPaciente { get; set; }
        public int idCiudad { get; set; }
        public int idTipoPaciente { get; set; }
        public int idEstPaciente { get; set; }

        public DateTime fecCreacion { get; set; }
    }
}
