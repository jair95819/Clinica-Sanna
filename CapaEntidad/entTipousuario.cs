using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entTipousuario
    {
        public int TipousuarioID { get; set; }  // PK
        public string Nombre { get; set; }
    }

}
