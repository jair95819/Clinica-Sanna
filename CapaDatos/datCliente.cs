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
    public class datCliente
    {
        #region singleton
        private static readonly datCliente UnicaInstancia = new datCliente();

        public static datCliente Instancia
        {
            get { return datCliente.UnicaInstancia; }
        }
        #endregion singleton

        #region metodos
        public List<entCliente> ListarCliente()
        {
            SqlCommand cmd = null;
            List<entCliente> lista = new List<entCliente>();

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();

                cmd = new SqlCommand("spListarCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    entCliente c = new entCliente();

                    c.idCliente = Convert.ToInt32(dr["idCliente"]);
                    c.rucCliente = Convert.ToString(dr["rucCliente"]);
                    c.razonSocial = Convert.ToString(dr["razonSocial"]);
                    c.dirCliente = Convert.ToString(dr["dirCliente"]);
                    c.idCiudad = Convert.ToInt32(dr["idCiudad"]);
                    c.idTipoCliente = Convert.ToInt32(dr["idTipoCliente"]);
                    c.idEstCliente = Convert.ToInt32(dr["idEstCliente"]);

                    lista.Add(c);
                }
            }
            catch (SqlException e)
            { throw e; }

            finally
            { cmd.Connection.Close(); }

            return lista;
        }

        public Boolean InsertarCliente(entCliente c)
        {
            SqlCommand cmd = null;
            Boolean inserta = false;
            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();

                cmd = new SqlCommand("spInsertarCliente", cn);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@rucCliente", c.rucCliente);
                cmd.Parameters.AddWithValue("@razonSocial", c.razonSocial);
                cmd.Parameters.AddWithValue("@dirCliente", c.dirCliente);
                cmd.Parameters.AddWithValue("@idCiudad", c.idCiudad);
                cmd.Parameters.AddWithValue("@idTipoCliente", c.idTipoCliente);
                cmd.Parameters.AddWithValue("@idEstCliente", c.idEstCliente);
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

        public Boolean EditarCliente(entCliente c)
        {
            SqlCommand cmd = null;
            Boolean edita = false;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();

                cmd = new SqlCommand("spEditarCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCliente", c.idCliente);
                cmd.Parameters.AddWithValue("@rucCliente", c.rucCliente);
                cmd.Parameters.AddWithValue("@razonSocial", c.razonSocial);
                cmd.Parameters.AddWithValue("@dirCliente", c.dirCliente);
                cmd.Parameters.AddWithValue("@idCiudad", c.idCiudad);
                cmd.Parameters.AddWithValue("@idTipoCliente", c.idTipoCliente);
                cmd.Parameters.AddWithValue("@idEstCliente", c.idEstCliente);
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

        public entCliente BuscarCliente(int id)
        {
            SqlCommand cmd = null;
            entCliente c = new entCliente();
            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spBuscarCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCliente", id);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    c.idCliente = Convert.ToInt32(dr["idCliente"]);
                    c.rucCliente = Convert.ToString(dr["rucCliente"]);
                    c.razonSocial = Convert.ToString(dr["razonSocial"]);
                    c.dirCliente = Convert.ToString(dr["dirCliente"]);
                    c.idCiudad = Convert.ToInt32(dr["idCiudad"]);
                    c.idTipoCliente = Convert.ToInt32(dr["idTipoCliente"]);
                    c.idEstCliente = Convert.ToInt32(dr["idEstCliente"]);
                    c.fecCreacion = Convert.ToDateTime(dr["fecCreacion"]);
                }
            }
            catch (SqlException e)
            { throw e; }
            finally
            { cmd.Connection.Close(); }
            return c;
        }

        public Boolean EliminarCliente(int id)
        {
            SqlCommand cmd = null;
            Boolean elimina = false;
            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spEliminarCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCliente", id);
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