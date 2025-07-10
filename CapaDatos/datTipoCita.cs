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
    public class datTipoCita
    {
        #region Singleton
        private static readonly datTipoCita instancia = new datTipoCita();
        public static datTipoCita Instancia => instancia;
        #endregion

        #region Método
        public List<entTipoCita> ListarTipoCita()
        {
            List<entTipoCita> lista = new List<entTipoCita>();
            SqlCommand cmd = null;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarTipoCita", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var tu = new entTipoCita
                    {
                        id = Convert.ToInt32(dr["TipocitaID"]),
                        nombre = Convert.ToString(dr["Nombre"])
                    };
                    lista.Add(tu);
                }
            }
            catch (Exception e)
            { throw e; }
            finally
            { cmd?.Connection?.Close(); }

            return lista;
        }

        #endregion
    }
}
