using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaLogica
{
    public class logTipousuario
    {
        #region Singleton
        private static readonly logTipousuario instancia = new logTipousuario();
        public static logTipousuario Instancia => instancia;
        #endregion

        #region Métodos
        public List<entTipousuario> ListarTipousuario()
        {
            return datTipousuario.Instancia.ListarTipousuario();
        }
        #endregion
    }
}
