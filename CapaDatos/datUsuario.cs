using System;
using System.Data.SqlClient;
using System.Data;
using CapaEntidad;

namespace CapaDatos
{
    public class datUsuario
    {
        #region singleton
        private static readonly datUsuario UnicaInstancia = new datUsuario();

        public static datUsuario Instancia
        {
            get { return datUsuario.UnicaInstancia; }
        }
        #endregion singleton

        public entUsuario ValidarLogin(string email, string password)
        {
            SqlCommand cmd = null;
            entUsuario usuario = null;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spValidarLogin", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password); // Asegúrate de encriptar las contraseñas

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    usuario = new entUsuario
                    {
                        UsuarioID = Convert.ToInt32(dr["UsuarioID"]),
                        Email = Convert.ToString(dr["Email"]),
                        Password = Convert.ToString(dr["Password"]),
                        TipoUsuario = Convert.ToString(dr["TipoUsuario"])
                    };
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cmd.Connection.Close();
            }

            return usuario;
        }
    }
}

