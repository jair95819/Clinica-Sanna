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
    public class datPago
    {
        #region Singleton
        private static readonly datPago instancia = new datPago();
        public static datPago Instancia => instancia;
        #endregion

        #region Métodos
        public bool InsertarPago(entPago m)
        {
            SqlCommand cmd = null;
            bool inserta = false;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertarPago", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@cita", m.idCita);
                cmd.Parameters.AddWithValue("@metodo", m.idMetodoPago);
                cmd.Parameters.AddWithValue("@monto", m.monto);
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
