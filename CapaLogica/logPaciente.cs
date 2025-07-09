using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaLogica
{
    public class logPaciente
    {
        #region singleton
        private static readonly logPaciente UnicaInstancia = new logPaciente();

        public static logPaciente Instancia
        {
            get { return logPaciente.UnicaInstancia;  }
        }
        #endregion singleton

        #region metodos
        public List<entPaciente> ListarPaciente()
        {
            List<entPaciente> lista = datPaciente.Instancia.ListarPaciente();
            return lista;
        }

        public Boolean InsertarPaciente(entPaciente c)
        {
            try
            {
                return datPaciente.Instancia.InsertarPaciente(c);
            }
            catch (Exception e)
            { throw e; }
        }

        public Boolean EditarPaciente(entPaciente c)
        {
            try
            {
                return datPaciente.Instancia.EditarPaciente(c);
            }
            catch (Exception e)
            { throw e; }
        }

        public entPaciente BuscarPaciente(int id)
        {
            try
            {
                return datPaciente.Instancia.BuscarPaciente(id);
            }
            catch (Exception e)
            { throw e; }
        }

        public Boolean DeshabilitarPaciente(int id)
        {
            return datPaciente.Instancia.DeshabilitarPaciente(id);
        }
        public entPaciente ObtenerPacientePorUsuarioID(int usuarioID)
        {
            try
            {
                return datPaciente.Instancia.ObtenerPacientePorUsuarioID(usuarioID);
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        #endregion metodos
    }
}   
