using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaLogica
{
    public class logMetodopago
    {
        private static readonly logMetodopago instancia = new logMetodopago();

        public static logMetodopago Instancia
        {
            get { return instancia; }
        }

        public List<entMetodopago> Listar()
        {
            return datMetodopago.Instancia.Listar();
        }

        public bool Insertar(entMetodopago mp)
        {
            return datMetodopago.Instancia.Insertar(mp);
        }
    }
}
