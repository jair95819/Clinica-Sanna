using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using CapaEntidad;

namespace CapaDatos
{
    public class datUsuario
    {
        #region Singleton
        private static readonly datUsuario instancia = new datUsuario();
        public static datUsuario Instancia => instancia;
        #endregion

        #region Métodos

        public List<entUsuario> ListarUsuario()
        {
            List<entUsuario> lista = new List<entUsuario>();
            SqlCommand cmd = null;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var u = new entUsuario
                    {
                        UsuarioID = Convert.ToInt32(dr["UsuarioID"]),
                        Email = Convert.ToString(dr["Email"]),
                        Password = Convert.ToString(dr["Password"]),
                        TipousuarioID = Convert.ToInt32(dr["TipousuarioID"]),
                        TipoUsuario = new entTipousuario
                        {
                            TipousuarioID = Convert.ToInt32(dr["TipousuarioID"]),
                            Nombre = Convert.ToString(dr["Nombre"])
                        }
                    };
                    lista.Add(u);
                }
            }
            catch (Exception e)
            { throw e; }
            finally
            { cmd?.Connection?.Close(); }

            return lista;
        }

        public bool InsertarUsuario(entUsuario u)
        {
            SqlCommand cmd = null;
            bool inserta = false;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertarUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Email", u.Email);
                cmd.Parameters.AddWithValue("@Password", u.Password);
                cmd.Parameters.AddWithValue("@TipousuarioID", u.TipousuarioID);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                inserta = (i > 0);
            }
            catch (Exception e)
            { throw e; }
            finally
            { cmd?.Connection?.Close(); }

            return inserta;
        }

        public bool EditarUsuario(entUsuario u)
        {
            SqlCommand cmd = null;
            bool edita = false;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spEditarUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UsuarioID", u.UsuarioID);
                cmd.Parameters.AddWithValue("@Email", u.Email);
                cmd.Parameters.AddWithValue("@Password", u.Password);
                cmd.Parameters.AddWithValue("@TipousuarioID", u.TipousuarioID);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                edita = (i > 0);
            }
            catch (Exception e)
            { throw e; }
            finally
            { cmd?.Connection?.Close(); }

            return edita;
        }

        public entUsuario BuscarUsuario(int id)
        {
            SqlCommand cmd = null;
            entUsuario u = null;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spBuscarUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UsuarioID", id);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    u = new entUsuario
                    {
                        UsuarioID = Convert.ToInt32(dr["UsuarioID"]),
                        Email = Convert.ToString(dr["Email"]),
                        Password = Convert.ToString(dr["Password"]),
                        TipousuarioID = Convert.ToInt32(dr["TipousuarioID"]),
                        TipoUsuario = new entTipousuario
                        {
                            TipousuarioID = Convert.ToInt32(dr["TipousuarioID"]),
                            Nombre = Convert.ToString(dr["Nombre"])
                        }
                    };
                }
            }
            catch (Exception e)
            { throw e; }
            finally
            { cmd?.Connection?.Close(); }

            return u;
        }

        public bool EliminarUsuario(int id)
        {
            SqlCommand cmd = null;
            bool elimina = false;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spEliminarUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UsuarioID", id);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                elimina = (i > 0);
            }
            catch (Exception e)
            { throw e; }
            finally
            { cmd?.Connection?.Close(); }

            return elimina;
        }

        public entUsuario ValidarUsuario(string email, string password, string tipoUsuario)
        {
            SqlCommand cmd = null;
            entUsuario usuario = null;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spValidarUsuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@TipoUsuario", tipoUsuario);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    usuario = new entUsuario
                    {
                        UsuarioID = Convert.ToInt32(dr["UsuarioID"]),
                        Email = dr["Email"].ToString(),
                        Password = dr["Password"].ToString(),
                        TipousuarioID = Convert.ToInt32(dr["TipousuarioID"]),
                        TipoUsuario = new entTipousuario
                        {
                            TipousuarioID = Convert.ToInt32(dr["TipousuarioID"]),
                            Nombre = dr["NombreTipo"].ToString()
                        }
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

        public int InsertarUsuarioYDevolverID(entUsuario u)
        {
            int nuevoID = 0;
            SqlCommand cmd = null;
            try
            {
                using (SqlConnection cn = conexion.Instancia.Conectar())
                {
                    cmd = new SqlCommand("spInsertarUsuario", cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", u.Email);
                    cmd.Parameters.AddWithValue("@Password", u.Password);
                    cmd.Parameters.AddWithValue("@TipousuarioID", u.TipousuarioID);

                    cn.Open();

                    // IMPORTANTE: Convert.ToInt32 para capturar el ID devuelto
                    nuevoID = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cmd?.Connection?.Close();
            }

            return nuevoID;
        }




        #endregion
    }
}
