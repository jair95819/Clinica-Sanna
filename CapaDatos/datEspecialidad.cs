﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using CapaEntidad;

namespace CapaDatos
{
    public class datEspecialidad
    {
        #region Singleton
        private static readonly datEspecialidad instancia = new datEspecialidad();
        public static datEspecialidad Instancia => instancia;
        #endregion

        #region Métodos

        public List<entEspecialidad> ListarEspecialidad()
        {
            List<entEspecialidad> lista = new List<entEspecialidad>();
            SqlCommand cmd = null;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarEspecialidad", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var e = new entEspecialidad
                    {
                        EspecialidadID = Convert.ToInt32(dr["EspecialidadID"]),
                        Nombre = Convert.ToString(dr["Nombre"]),
                        Estado = Convert.ToBoolean(dr["Estado"])
                    };
                    lista.Add(e);
                }
            }
            catch (Exception e)
            { throw e; }
            finally
            { cmd?.Connection?.Close(); }

            return lista;
        }

        public bool InsertarEspecialidad(entEspecialidad e)
        {
            SqlCommand cmd = null;
            bool inserta = false;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spInsertarEspecialidad", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre", e.Nombre);
                cmd.Parameters.AddWithValue("@Estado", e.Estado);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                inserta = (i > 0);
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { cmd?.Connection?.Close(); }

            return inserta;
        }

        public bool EditarEspecialidad(entEspecialidad e)
        {
            SqlCommand cmd = null;
            bool edita = false;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spEditarEspecialidad", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EspecialidadID", e.EspecialidadID);
                cmd.Parameters.AddWithValue("@Nombre", e.Nombre);
                cmd.Parameters.AddWithValue("@Estado", e.Estado);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                edita = (i > 0);
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { cmd?.Connection?.Close(); }

            return edita;
        }

        public entEspecialidad BuscarEspecialidad(int id)
        {
            SqlCommand cmd = null;
            entEspecialidad e = null;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spBuscarEspecialidad", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EspecialidadID", id);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    e = new entEspecialidad
                    {
                        EspecialidadID = Convert.ToInt32(dr["EspecialidadID"]),
                        Nombre = Convert.ToString(dr["Nombre"]),
                        Estado = Convert.ToBoolean(dr["Estado"])
                    };
                }
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { cmd?.Connection?.Close(); }

            return e;
        }

        public bool DeshabilitarEspecialidad(int id)
        {
            SqlCommand cmd = null;
            bool deshabilita = false;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spEliminarEspecialidad", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EspecialidadID", id);

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                deshabilita = (i > 0);
            }
            catch (Exception ex)
            { throw ex; }
            finally
            { cmd?.Connection?.Close(); }

            return deshabilita;
        }
        public List<entEspecialidad> ListarEspecialidadesActivas()
        {
            List<entEspecialidad> especialidades = new List<entEspecialidad>();
            SqlCommand cmd = null;

            try
            {
                SqlConnection cn = conexion.Instancia.Conectar();
                cmd = new SqlCommand("spListarEspecialidadesActivas", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var especialidad = new entEspecialidad
                    {
                        EspecialidadID = Convert.ToInt32(reader["EspecialidadID"]),
                        Nombre = Convert.ToString(reader["Nombre"]),
                        Estado = Convert.ToInt32(reader["Estado"]) == 1 ? true : false // Se asegura de convertir el estado a un booleano
                    };
                    especialidades.Add(especialidad);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las especialidades activas.", ex);
            }
            finally
            { cmd?.Connection?.Close(); }

            return especialidades;
        }


        #endregion

    }
}
