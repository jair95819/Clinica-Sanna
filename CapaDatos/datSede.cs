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
    public class datSede
    {
        #region Singleton
        private static readonly datSede instancia = new datSede();
        public static datSede Instancia => instancia;
        #endregion Singleton

        #region Métodos

        // Método para listar todas las sedes
        public List<entSede> ListarSede()
        {
            SqlCommand cmd = null;
            List<entSede> lista = new List<entSede>();

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarSede", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var sede = new entSede
                    {
                        idSede = Convert.ToInt32(dr["SedeID"]),
                        nombre = Convert.ToString(dr["Nombre"]),
                        direccion = Convert.ToString(dr["Direccion"])
                    };
                    lista.Add(sede);
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                cmd?.Connection?.Close();
            }

            return lista;
        }

        // Método para insertar una nueva sede
        public Boolean InsertarSede(entSede sede)
        {
            SqlCommand cmd = null;
            bool inserta = false;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertarSede", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", sede.nombre);
                cmd.Parameters.AddWithValue("@Direccion", sede.direccion);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0) { inserta = true; }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cmd?.Connection?.Close();
            }

            return inserta;
        }

        // Método para editar una sede existente
        public Boolean EditarSede(entSede sede)
        {
            SqlCommand cmd = null;
            bool edita = false;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spEditarSede", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SedeID", sede.idSede);
                cmd.Parameters.AddWithValue("@Nombre", sede.nombre);
                cmd.Parameters.AddWithValue("@Direccion", sede.direccion);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0) { edita = true; }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cmd?.Connection?.Close();
            }

            return edita;
        }

        // Método para buscar una sede por su ID
        public entSede BuscarSede(int id)
        {
            SqlCommand cmd = null;
            entSede sede = null;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spBuscarSede", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SedeID", id);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    sede = new entSede
                    {
                        idSede = Convert.ToInt32(dr["SedeID"]),
                        nombre = Convert.ToString(dr["Nombre"]),
                        direccion = Convert.ToString(dr["Direccion"])
                    };
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                cmd?.Connection?.Close();
            }

            return sede;
        }

        // Método para deshabilitar una sede (cambiar el estado)
        public Boolean DeshabilitarSede(int id)
        {
            SqlCommand cmd = null;
            bool deshabilita = false;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spDeshabilitarSede", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SedeID", id);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0) { deshabilita = true; }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                cmd?.Connection?.Close();
            }

            return deshabilita;
        }

        #endregion Métodos
    }
}
