using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaLogica
{
    public class logUsuario
    {
        #region Singleton
        private static readonly logUsuario instancia = new logUsuario();
        public static logUsuario Instancia => instancia;
        #endregion

        #region Métodos
        public List<entUsuario> ListarUsuario()
        {
            return datUsuario.Instancia.ListarUsuario();
        }

        public bool InsertarUsuario(entUsuario u)
        {
            return datUsuario.Instancia.InsertarUsuario(u);
        }

        public bool EditarUsuario(entUsuario u)
        {
            return datUsuario.Instancia.EditarUsuario(u);
        }

        public entUsuario BuscarUsuario(int id)
        {
            return datUsuario.Instancia.BuscarUsuario(id);
        }

        public bool EliminarUsuario(int id)
        {
            return datUsuario.Instancia.EliminarUsuario(id);
        }
        #endregion
        public entUsuario ValidarUsuario(string email, string password, string tipoUsuario)
        {
            var usuario = datUsuario.Instancia.ValidarUsuario(email, password, tipoUsuario);

            return usuario; // puede ser null si no existe
        }
        public int InsertarUsuarioYDevolverID(entUsuario u)
        {
            return datUsuario.Instancia.InsertarUsuarioYDevolverID(u);
        }

    }
}
