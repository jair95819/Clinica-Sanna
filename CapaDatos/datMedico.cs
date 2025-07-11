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
    public class datMedico
    {
        #region Singleton
        private static readonly datMedico instancia = new datMedico();
        public static datMedico Instancia => instancia;
        #endregion

        #region Métodos

        public List<entMedico> ListarMedico()
        {
            List<entMedico> lista = new List<entMedico>();
            SqlCommand cmd = null;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarMedico", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var m = new entMedico
                    {
                        MedicoID = Convert.ToInt32(dr["MedicoID"]),
                        Nombres = Convert.ToString(dr["Nombre"]),
                        Apellidos = Convert.ToString(dr["Apellido"]),
                        Telefono = Convert.ToString(dr["Telefono"]),
                        Estado = Convert.ToBoolean(dr["Estado"]),
                        UsuarioID = Convert.ToInt32(dr["UsuarioID"]),
                        EspecialidadID = Convert.ToInt32(dr["EspecialidadID"])
                    };
                    lista.Add(m);
                }
            }
            catch (Exception e)
            { throw e; }
            finally
            { cmd?.Connection?.Close(); }

            return lista;
        }

        public bool InsertarMedico(entMedico m)
        {
            SqlCommand cmd = null;
            bool inserta = false;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertarMedico", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombres", m.Nombres);
                cmd.Parameters.AddWithValue("@Apellidos", m.Apellidos);
                cmd.Parameters.AddWithValue("@Telefono", m.Telefono);
                cmd.Parameters.AddWithValue("@Estado", m.Estado);
                cmd.Parameters.AddWithValue("@UsuarioID", m.UsuarioID);
                cmd.Parameters.AddWithValue("@EspecialidadID", m.EspecialidadID);

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

        public bool EditarMedico(entMedico m)
        {
            SqlCommand cmd = null;
            bool edita = false;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spEditarMedico", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MedicoID", m.MedicoID);
                cmd.Parameters.AddWithValue("@Nombres", m.Nombres);
                cmd.Parameters.AddWithValue("@Apellidos", m.Apellidos);
                cmd.Parameters.AddWithValue("@Telefono", m.Telefono);
                cmd.Parameters.AddWithValue("@Estado", m.Estado);
                cmd.Parameters.AddWithValue("@UsuarioID", m.UsuarioID);
                cmd.Parameters.AddWithValue("@EspecialidadID", m.EspecialidadID);

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

        public entMedico BuscarMedico(int id)
        {
            SqlCommand cmd = null;
            entMedico m = null;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spBuscarMedico", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MedicoID", id);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    m = new entMedico
                    {
                        MedicoID = Convert.ToInt32(dr["MedicoID"]),
                        Nombres = Convert.ToString(dr["Nombre"]),
                        Apellidos = Convert.ToString(dr["Apellido"]),
                        Telefono = Convert.ToString(dr["Telefono"]),
                        Estado = Convert.ToBoolean(dr["Estado"]),
                        UsuarioID = Convert.ToInt32(dr["UsuarioID"]),
                        EspecialidadID = Convert.ToInt32(dr["EspecialidadID"])
                    };
                }
            }
            catch (Exception e)
            { throw e; }
            finally
            { cmd?.Connection?.Close(); }

            return m;
        }

        public bool DeshabilitarMedico(int id)
        {
            SqlCommand cmd = null;
            bool deshabilita = false;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spDeshabilitarMedico", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MedicoID", id);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                deshabilita = (i > 0);
            }
            catch (Exception e)
            { throw e; }
            finally
            { cmd?.Connection?.Close(); }

            return deshabilita;
        }

        public entMedico ObtenerMedicoPorUsuarioID(int usuarioID)
        {
            SqlCommand cmd = null;
            entMedico medico = null;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spObtenerMedicoPorUsuarioID", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    medico = new entMedico
                    {
                        MedicoID = Convert.ToInt32(dr["MedicoID"]),
                        UsuarioID = Convert.ToInt32(dr["UsuarioID"]),
                        Nombres = Convert.ToString(dr["Nombre"]),
                        Apellidos = Convert.ToString(dr["Apellido"]),
                        Telefono = Convert.ToString(dr["Telefono"]),
                        Estado = Convert.ToBoolean(dr["Estado"]),
                        EspecialidadID = Convert.ToInt32(dr["EspecialidadID"])
                    };
                }
            }
            catch (Exception e)
            { throw e; }
            finally
            { cmd?.Connection?.Close(); }

            return medico;
        }

        #endregion
    }
}
