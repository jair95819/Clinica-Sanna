using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logCita
    {
        #region Singleton
        private static readonly logCita instancia = new logCita();
        public static logCita Instancia => instancia;
        #endregion

        // Método para listar todas las citas
        public List<entCita> ListarCita()
        {
            return datCita.Instancia.ListarCita();
        }

        // Método para insertar una nueva cita
        public bool InsertarCita(entCita cita)
        {
            try
            {
                // Llamamos a la capa de datos para insertar la cita
                return datCita.Instancia.InsertarCita(cita);
            }
            catch (Exception ex)
            {
                // Puedes manejar los errores aquí si es necesario
                throw new Exception("Error al insertar la cita: " + ex.Message);
            }
        }
    }
}
