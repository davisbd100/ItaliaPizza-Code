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

        //Permite realizar pruebas agregar con FalloSQL
        public Empleado(string idPersona, string nombre, string apellido, string telefono, string email, string calle, string numero, string codigoPostal, string colonia, string ciudad, string idEmpleado, string NombreUsuario, string Contraseña, DateTime FechaUltimoAcceso, string TipoEmpleado)
        {
            this.idPersona = idPersona;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Telefono = telefono;
            this.Email = email;
            this.Calle = calle;
            this.Numero = numero;
            this.CodigoPostal = codigoPostal;
            this.Colonia = colonia;
            this.Ciudad = ciudad;
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
