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
    public class datTipousuario
    {
        #region Singleton
        private static readonly datTipousuario instancia = new datTipousuario();
        public static datTipousuario Instancia => instancia;
        #endregion

        #region Métodos

        public List<entTipousuario> ListarTipousuario()
        {
            List<entTipousuario> lista = new List<entTipousuario>();
            SqlCommand cmd = null;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarTipousuario", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var tu = new entTipousuario
                    {
                        TipousuarioID = Convert.ToInt32(dr["TipousuarioID"]),
                        Nombre = Convert.ToString(dr["Nombre"])
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
