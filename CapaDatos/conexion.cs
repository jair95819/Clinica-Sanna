﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace CapaDatos
{
    public class conexion
    {
        #region singleton
        private static readonly conexion UnicaInstancia = new conexion();

        public static conexion Instancia
        {
            get { return conexion.UnicaInstancia; }
        }
        #endregion singleton

        public SqlConnection Conectar()
        {
            SqlConnection cn = new SqlConnection();

            cn.ConnectionString = "Data Source=DESKTOP-5J5OED0\\SQLEXPRESS;initial Catalog=ClinicaSanna;" + "Integrated Security=true";
            return cn;
        }
    }
}