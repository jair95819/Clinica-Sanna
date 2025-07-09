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
    public class logUsuario
    {
        #region singleton
        private static readonly logUsuario UnicaInstancia = new logUsuario();

        public static logUsuario Instancia
        {
            get { return logUsuario.UnicaInstancia; }
        }
        #endregion singleton

        public entUsuario ValidarLogin(string email, string password)
        {
            return datUsuario.Instancia.ValidarLogin(email, password);  // Llamada al método de la capa de datos
        }
    }
}
