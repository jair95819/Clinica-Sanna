using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace CapaDatos
{
    public class datPaciente


    {
        #region singleton
        private static readonly datPaciente UnicaInstancia = new datPaciente();

        public static datPaciente Instancia
        {
            get { return datPaciente.UnicaInstancia; }
        }
        #endregion singleton

        #region metodos
        public List<entPaciente> ListarPaciente()
        {
            SqlCommand cmd = null;
            List<entPaciente> lista = new List<entPaciente>();

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();

                cmd = new SqlCommand("spListarPaciente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    entPaciente c = new entPaciente();

                    c.idPaciente = Convert.ToInt32(dr["idPaciente"]);
                    c.rucPaciente = Convert.ToString(dr["rucPaciente"]);
                    c.razonSocial = Convert.ToString(dr["razonSocial"]);
                    c.dirPaciente = Convert.ToString(dr["dirPaciente"]);
                    c.idCiudad = Convert.ToInt32(dr["idCiudad"]);
                    c.idTipoPaciente = Convert.ToInt32(dr["idTipoPaciente"]);
                    c.idEstPaciente = Convert.ToInt32(dr["idEstPaciente"]);

                    lista.Add(c);
                }
            }
            catch (SqlException e)
            { throw e; }

            finally
            { cmd.Connection.Close(); }

            return lista;
        }

        public Boolean InsertarPaciente(entPaciente c)
        {
            SqlCommand cmd = null;
            Boolean inserta = false;
            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();

                cmd = new SqlCommand("spInsertarPaciente", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@rucPaciente", c.rucPaciente);
                cmd.Parameters.AddWithValue("@razonSocial", c.razonSocial);
                cmd.Parameters.AddWithValue("@dirPaciente", c.dirPaciente);
                cmd.Parameters.AddWithValue("@idCiudad", c.idCiudad);
                cmd.Parameters.AddWithValue("@idTipoPaciente", c.idTipoPaciente);
                cmd.Parameters.AddWithValue("@idEstPaciente", c.idEstPaciente);
                cmd.Parameters.AddWithValue("@fecCreacion", c.fecCreacion);

                cn.Open();

                int i = cmd.ExecuteNonQuery();
                if (i > 0) { inserta = true; }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally { cmd.Connection.Close(); }

            return inserta;

        }

        public Boolean EditarPaciente(entPaciente c)
        {
            SqlCommand cmd = null;
            Boolean edita = false;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();

                cmd = new SqlCommand("spEditarPaciente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idPaciente", c.idPaciente);
                cmd.Parameters.AddWithValue("@rucPaciente", c.rucPaciente);
                cmd.Parameters.AddWithValue("@razonSocial", c.razonSocial);
                cmd.Parameters.AddWithValue("@dirPaciente", c.dirPaciente);
                cmd.Parameters.AddWithValue("@idCiudad", c.idCiudad);
                cmd.Parameters.AddWithValue("@idTipoPaciente", c.idTipoPaciente);
                cmd.Parameters.AddWithValue("@idEstPaciente", c.idEstPaciente);
                cmd.Parameters.AddWithValue("@fecCreacion", c.fecCreacion);

                cn.Open();

                int i = cmd.ExecuteNonQuery();

                if (i > 0) { edita = true; }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally { cmd.Connection.Close(); }

            return edita;

        }

        public entPaciente BuscarPaciente(int id)
        {
            SqlCommand cmd = null;
            entPaciente c = new entPaciente();
            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spBuscarPaciente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idPaciente", id);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    c.idPaciente = Convert.ToInt32(dr["idPaciente"]);
                    c.rucPaciente = Convert.ToString(dr["rucPaciente"]);
                    c.razonSocial = Convert.ToString(dr["razonSocial"]);
                    c.dirPaciente = Convert.ToString(dr["dirPaciente"]);
                    c.idCiudad = Convert.ToInt32(dr["idCiudad"]);
                    c.idTipoPaciente = Convert.ToInt32(dr["idTipoPaciente"]);
                    c.idEstPaciente = Convert.ToInt32(dr["idEstPaciente"]);
                    c.fecCreacion = Convert.ToDateTime(dr["fecCreacion"]);
                }
            }
            catch (SqlException e)
            { throw e; }
            finally
            { cmd.Connection.Close(); }
            return c;
        }

        public Boolean EliminarPaciente(int id)
        {
            SqlCommand cmd = null;
            Boolean elimina = false;
            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spEliminarPaciente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idPaciente", id);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0) { elimina = true; }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally { cmd.Connection.Close(); }
            return elimina;
        }
        #endregion metodos
    }
}