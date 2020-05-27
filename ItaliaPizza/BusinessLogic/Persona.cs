using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Persona
    {
        public String idPersona { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public String Telefono { get; set; }
        public String Email { get; set; }
        public String Calle { get; set; }
        public String Numero { get; set; }
        public String CodigoPostal { get; set; }
        public String Colonia { get; set; }
        public String Ciudad { get; set; }

        public Persona(string idPersona, string nombre, string apellido, string telefono, string email, string calle, string numero, string codigoPostal, string colonia, string ciudad)
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
        }

        public Persona()
        {

        }
    }
}
