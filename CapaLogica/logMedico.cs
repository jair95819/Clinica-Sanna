using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaLogica
{
    public class logMedico
    {
        #region Singleton
        private static readonly logMedico instancia = new logMedico();
        public static logMedico Instancia => instancia;
        #endregion

        #region Métodos
        public List<entMedico> ListarMedico()
        {
            return datMedico.Instancia.ListarMedico();
        }

        public bool InsertarMedico(entMedico m)
        {
            return datMedico.Instancia.InsertarMedico(m);
        }

        public bool EditarMedico(entMedico m)
        {
            return datMedico.Instancia.EditarMedico(m);
        }

        public entMedico BuscarMedico(int id)
        {
            return datMedico.Instancia.BuscarMedico(id);
        }

        public bool DeshabilitarMedico(int id)
        {
            return datMedico.Instancia.DeshabilitarMedico(id);
        }
        public entMedico ObtenerMedicoPorUsuarioID(int usuarioID)
        {
            try
            {
                return datMedico.Instancia.ObtenerMedicoPorUsuarioID(usuarioID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
