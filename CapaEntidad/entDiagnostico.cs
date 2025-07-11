using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entDiagnostico
    {
        public int id { get; set; }
        public int idMedico { get; set; }
        public string sintomas { get; set; }
        public string observaciones { get; set; }
        public DateTime fecha { get; set; }
    }
}
