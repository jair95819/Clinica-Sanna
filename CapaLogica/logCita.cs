using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logCita
    {
        #region Singleton
        private static readonly logCita instancia = new logCita();
        public static logCita Instancia => instancia;
        #endregion

        public List<entCita> ListarCita()
        {
            return datCita.Instancia.ListarCita();
        }

    }
}
