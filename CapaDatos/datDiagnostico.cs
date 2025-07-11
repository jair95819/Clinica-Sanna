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
    public class datDiagnostico
    {
        #region Singleton
        private static readonly datDiagnostico instancia = new datDiagnostico();
        public static datDiagnostico Instancia => instancia;
        #endregion

        #region Métodos
        public List<entDiagnostico> ListarDiagnostico()
        {
            List<entDiagnostico> lista = new List<entDiagnostico>();
            SqlCommand cmd = null;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarDiagnostico", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var m = new entDiagnostico()
                    {
                        id = Convert.ToInt32(dr["DiagnosticoID"]),
                        idMedico = Convert.ToInt32(dr["MedicoID"]),
                        sintomas = Convert.ToString(dr["Sintomas"]),
                        observaciones = Convert.ToString(dr["Observaciones"]),
                        fecha = Convert.ToDateTime(dr["fechaDiagnostico"]),
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

        public bool InsertarDiagnostico(entDiagnostico m)
        {
            SqlCommand cmd = null;
            bool inserta = false;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertarDiagnostico", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@medico", m.idMedico);
                cmd.Parameters.AddWithValue("@sintomas", m.sintomas);
                cmd.Parameters.AddWithValue("@observaciones", m.observaciones);
                cmd.Parameters.AddWithValue("@fecha", DateTime.Now);

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
        #endregion
    }
}
