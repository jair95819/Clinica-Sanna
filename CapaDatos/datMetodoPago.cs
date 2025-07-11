using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class datMetodopago
    {
        // 🔧 Singleton
        private static readonly datMetodopago _instancia = new datMetodopago();
        public static datMetodopago Instancia
        {
            get { return _instancia; }
        }

        public List<entMetodopago> Listar()
        {
            List<entMetodopago> lista = new List<entMetodopago>();
            SqlCommand cmd = null;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("SELECT * FROM Metodopago", cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    entMetodopago mp = new entMetodopago
                    {
                        MetodopagoID = Convert.ToInt32(dr["MetodopagoID"]),
                        Nombre = dr["Nombre"].ToString()
                    };
                    lista.Add(mp);
                }

                dr.Close();
            }
            catch (Exception e)
            {
                throw e;
            }

            return lista;
        }

        public bool Insertar(entMetodopago mp)
        {
            SqlCommand cmd = null;
            bool insertado = false;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("INSERT INTO Metodopago (Nombre) VALUES (@Nombre)", cn);
                cmd.Parameters.AddWithValue("@Nombre", mp.Nombre);
                cn.Open();
                int filas = cmd.ExecuteNonQuery();
                insertado = filas > 0;
            }
            catch (Exception e)
            {
                throw e;
            }

            return insertado;
        }
    }
}
