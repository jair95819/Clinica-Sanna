using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logPago
    {
        #region Singleton
        private static readonly logPago instancia = new logPago();
        public static logPago Instancia => instancia;
        #endregion

        #region Métodos
        public bool InsertarPago(entPago p)
        {
            return datPago.Instancia.InsertarPago(p);
        }
        #endregion
    }
}
