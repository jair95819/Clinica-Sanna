﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class entMedico
    {
        public int MedicoID { get; set; }             // PK
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public bool Estado { get; set; }

        public int UsuarioID { get; set; }            // FK a Usuario
        public entUsuario Usuario { get; set; }       // navegación

        public int EspecialidadID { get; set; }       // FK a Especialidad
        public entEspecialidad Especialidad { get; set; } // navegación
    }

}