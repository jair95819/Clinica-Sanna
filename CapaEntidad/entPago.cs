using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entPago
    {
        public int id { get; set; }
        public int idCita { get; set; }
        public int idMetodoPago {  get; set; }
        public double monto { get; set; }
        public DateTime fecha { get; set; }
    }
}
