using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class datCita
    {
        #region Singleton
        private static readonly datCita instancia = new datCita();
        public static datCita Instancia => instancia;
        #endregion

        #region Métodos
        // Método para listar todas las citas
        public List<entCita> ListarCita()
        {
            List<entCita> lista = new List<entCita>();
            SqlCommand cmd = null;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarCita", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var m = new entCita
                    {
                        idCita = Convert.ToInt32(dr["CitaID"]),
                        idSede = Convert.ToInt32(dr["SedeID"]),
                        tCita = Convert.ToInt32(dr["TipoCitaID"]),
                        idMedico = Convert.ToInt32(dr["MedicoID"]),
                        fCita = Convert.ToDateTime(dr["FechaCita"]),
                        estCita = Convert.ToString(dr["Estado"]),
                        consCita = Convert.ToString(dr["Consultorio"]),
                    };
                    lista.Add(m);
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

            return lista;
        }

        // Método para insertar una nueva cita
        public bool InsertarCita(entCita m)
        {
            SqlCommand cmd = null;
            bool inserta = false;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertarCita", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // Añadir parámetros para el procedimiento almacenado
                cmd.Parameters.AddWithValue("@paciente", m.PacienteID);
                cmd.Parameters.AddWithValue("@medico", m.idMedico);
                cmd.Parameters.AddWithValue("@tipo", m.tCita);
                cmd.Parameters.AddWithValue("@sede", m.idSede);
                cmd.Parameters.AddWithValue("@cons", m.consCita);
                cmd.Parameters.AddWithValue("@est", m.estCita);
                cmd.Parameters.AddWithValue("@fecha", m.fCita);
                cmd.Parameters.AddWithValue("@fechacreacion", DateTime.Now);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                inserta = (i > 0); // Si se inserta, devuelve true
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
        #endregion
    }
}
