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
    public class logCliente
    {
        #region singleton
        private static readonly logCliente UnicaInstancia = new logCliente();

        public static logCliente Instancia
        {
            get { return logCliente.UnicaInstancia;  }
        }
        #endregion singleton

        #region metodos
        public List<entCliente> ListarCliente()
        {
            List<entCliente> lista = datCliente.Instancia.ListarCliente();
            return lista;
        }

        public Boolean InsertarCliente(entCliente c)
        {
            try
            {
                return datCliente.Instancia.InsertarCliente(c);
            }
            catch (Exception e)
            { throw e; }
        }

        public Boolean EditarCliente(entCliente c)
        {
            try
            {
                return datCliente.Instancia.EditarCliente(c);
            }
            catch (Exception e)
            { throw e; }
        }

        public entCliente BuscarCliente(int id)
        {
            try
            {
                return datCliente.Instancia.BuscarCliente(id);
            }
            catch (Exception e)
            { throw e; }
        }

        public Boolean EliminarCliente(int id)
        {
            try
            {
                return datCliente.Instancia.EliminarCliente(id);
            }
            catch (Exception e)
            { throw e; }
        }
        #endregion metodos
    }
}   
