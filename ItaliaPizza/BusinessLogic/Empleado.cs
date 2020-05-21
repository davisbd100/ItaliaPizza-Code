﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Empleado : Persona
    {
        public TipoEmpleado TipoEmpleado { get; set; }
        public String NombreUsuario { get; set; }
        public String Contraseña { get; set; }
        public DateTime FechaUltimoAcceso { get; set; }

        public Empleado(TipoEmpleado TipoEmpleado, string NombreUsuario, string Contraseña, DateTime FechaUltimoAcceso)
        {
            this.TipoEmpleado = TipoEmpleado;
            this.NombreUsuario = NombreUsuario;
            this.Contraseña = Contraseña;
            this.FechaUltimoAcceso = FechaUltimoAcceso;
        }

        public Empleado()
        {

        }
    }
}