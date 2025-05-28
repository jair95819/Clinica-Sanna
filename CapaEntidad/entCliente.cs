using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entCliente
    {
        public int idCliente { get; set; }
        public string rucCliente { get; set; }
        public string razonSocial { get; set; }
        public string dirCliente { get; set; }
        public int idCiudad { get; set; }
        public int idTipoCliente { get; set; }
        public int idEstCliente { get; set; }

        public DateTime fecCreacion { get; set; }
    }
}
