using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
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
                    var c = new entPaciente
                    {
                        PacienteID = Convert.ToInt32(dr["PacienteID"]),
                        Nombres = Convert.ToString(dr["Nombre"]),
                        Apellidos = Convert.ToString(dr["Apellido"]),
                        NumDoc = Convert.ToString(dr["NumDoc"]),
                        FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                        Telefono = Convert.ToString(dr["Telefono"]),
                        Sexo = Convert.ToString(dr["Sexo"]),
                        Estado = Convert.ToBoolean(dr["Estado"]),
                        UsuarioID = Convert.ToInt32(dr["UsuarioID"])
                    };
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
                cmd.Parameters.AddWithValue("@Nombres", c.Nombres);
                cmd.Parameters.AddWithValue("@Apellidos", c.Apellidos);
                cmd.Parameters.AddWithValue("@NumDoc", c.NumDoc);
                cmd.Parameters.AddWithValue("@FechaNacimiento", c.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Telefono", c.Telefono);
                cmd.Parameters.AddWithValue("@Sexo", c.Sexo);
                cmd.Parameters.AddWithValue("@Estado", c.Estado);
                cmd.Parameters.AddWithValue("@UsuarioID", c.UsuarioID);


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

                cmd.Parameters.AddWithValue("@PacienteID", c.PacienteID);
                cmd.Parameters.AddWithValue("@Nombres", c.Nombres);
                cmd.Parameters.AddWithValue("@Apellidos", c.Apellidos);
                cmd.Parameters.AddWithValue("@NumDoc", c.NumDoc);
                cmd.Parameters.AddWithValue("@FechaNacimiento", c.FechaNacimiento);
                cmd.Parameters.AddWithValue("@Telefono", c.Telefono);
                cmd.Parameters.AddWithValue("@Sexo", c.Sexo);
                cmd.Parameters.AddWithValue("@Estado", c.Estado);
                


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
                cmd.Parameters.AddWithValue("@PacienteID", id);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    c = new entPaciente
                    {
                        PacienteID = Convert.ToInt32(dr["PacienteID"]),
                        Nombres = Convert.ToString(dr["Nombre"]),
                        Apellidos = Convert.ToString(dr["Apellido"]),
                        NumDoc = Convert.ToString(dr["NumDoc"]),
                        FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                        Telefono = Convert.ToString(dr["Telefono"]),
                        Sexo = Convert.ToString(dr["Sexo"]),
                        Estado = Convert.ToBoolean(dr["Estado"]),
                        UsuarioID = Convert.ToInt32(dr["UsuarioID"])
                    };
                }
            }
            catch (SqlException e)
            { throw e; }
            finally
            { cmd?.Connection?.Close(); }

            return c;
        }

        public Boolean DeshabilitarPaciente(int id)
        {
            SqlCommand cmd = null;
            Boolean deshabilita = false;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spDeshabilitarPaciente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PacienteID", id);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                if (i > 0) deshabilita = true;
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

        public entPaciente ObtenerPacientePorUsuarioID(int usuarioID)
        {
            SqlCommand cmd = null;
            entPaciente paciente = null;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spObtenerPacientePorUsuarioID", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UsuarioID", usuarioID);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    paciente = new entPaciente
                    {
                        PacienteID = Convert.ToInt32(dr["PacienteID"]),
                        UsuarioID = Convert.ToInt32(dr["UsuarioID"]),
                        Nombres = Convert.ToString(dr["Nombre"]),
                        Apellidos = Convert.ToString(dr["Apellido"]),
                        NumDoc = Convert.ToString(dr["NumDoc"]),
                        FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                        Telefono = Convert.ToString(dr["Telefono"]),
                        Sexo = Convert.ToString(dr["Sexo"]),
                        Estado = Convert.ToBoolean(dr["Estado"])
                    };
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

            return paciente;
        }


        #endregion metodos
    }
}