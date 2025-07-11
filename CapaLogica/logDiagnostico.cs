using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logDiagnostico
    {
        #region Singleton
        private static readonly logDiagnostico instancia = new logDiagnostico();
        public static logDiagnostico Instancia => instancia;
        #endregion

        public List<entDiagnostico> ListarDiagnostico()
        {
            return datDiagnostico.Instancia.ListarDiagnostico();
        }

        public bool InsertarDiagnostico(entDiagnostico m)
        {
            return datDiagnostico.Instancia.InsertarDiagnostico(m);
        }
    }
}
