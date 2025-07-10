using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logTipoCita
    {
        #region Singleton
        private static readonly logTipoCita instancia = new logTipoCita();
        public static logTipoCita Instancia => instancia;
        #endregion

        #region Métodos
        public List<entTipoCita> ListarTipoCita()
        {
            return datTipoCita.Instancia.ListarTipoCita();
        }
        #endregion
    }
}
