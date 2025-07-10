using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaLogica
{
    public class logEspecialidad
    {
        #region Singleton
        private static readonly logEspecialidad instancia = new logEspecialidad();
        public static logEspecialidad Instancia => instancia;
        #endregion

        #region Métodos
        public List<entEspecialidad> ListarEspecialidad()
        {
            return datEspecialidad.Instancia.ListarEspecialidad();
        }

        public bool InsertarEspecialidad(entEspecialidad e)
        {
            return datEspecialidad.Instancia.InsertarEspecialidad(e);
        }

        public bool EditarEspecialidad(entEspecialidad e)
        {
            return datEspecialidad.Instancia.EditarEspecialidad(e);
        }

        public entEspecialidad BuscarEspecialidad(int id)
        {
            return datEspecialidad.Instancia.BuscarEspecialidad(id);
        }

        public bool DeshabilitarEspecialidad(int id)
        {
            return datEspecialidad.Instancia.DeshabilitarEspecialidad(id);
        }
        #endregion
    }
}
