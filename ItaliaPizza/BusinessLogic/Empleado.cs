using System;

namespace BusinessLogic
{
    public class Empleado : Persona
    {
        public String idEmpleado { get; set; }
        public String NombreUsuario { get; set; }
        public String Contraseña { get; set; }
        public DateTime FechaUltimoAcceso { get; set; }
        public String TipoEmpleado { get; set; }

        public Empleado(string idEmpleado, string NombreUsuario, string Contraseña, DateTime FechaUltimoAcceso, string TipoEmpleado)
        {
            this.idPersona = idEmpleado;
            this.NombreUsuario = NombreUsuario;
            this.Contraseña = Contraseña;
            this.FechaUltimoAcceso = FechaUltimoAcceso;
            this.TipoEmpleado = TipoEmpleado;
        }
        public Empleado()
        {

        }
    }
}
