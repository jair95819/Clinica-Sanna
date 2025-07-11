using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaLogica
{
    public class logSede
    {
        #region singleton
        private static readonly logSede UnicaInstancia = new logSede();

        public static logSede Instancia
        {
            get { return logSede.UnicaInstancia; }
        }
        #endregion singleton

        #region metodos
        // Método para listar todas las sedes
        public List<entSede> ListarSede()
        {
            List<entSede> lista = datSede.Instancia.ListarSede();
            return lista;
        }

        // Método para insertar una nueva sede
        public Boolean InsertarSede(entSede sede)
        {
            try
            {
                return datSede.Instancia.InsertarSede(sede);
            }
            catch (Exception e)
            { throw e; }
        }

        // Método para editar una sede existente
        public Boolean EditarSede(entSede sede)
        {
            try
            {
                return datSede.Instancia.EditarSede(sede);
            }
            catch (Exception e)
            { throw e; }
        }

        // Método para buscar una sede por su ID
        public entSede BuscarSede(int id)
        {
            try
            {
                return datSede.Instancia.BuscarSede(id);
            }
            catch (Exception e)
            { throw e; }
        }

        // Método para deshabilitar una sede (cambiar el estado)
        public Boolean DeshabilitarSede(int id)
        {
            return datSede.Instancia.DeshabilitarSede(id);
        }

        #endregion metodos
    }
}
