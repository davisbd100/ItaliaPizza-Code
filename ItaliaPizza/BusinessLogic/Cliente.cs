using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Cliente : Persona
    {
        public String idCliente { get; set; }

        public Cliente(string idCliente)
        {
            this.idCliente = idCliente;
        }

        public Cliente()
        {

        }

        public Cliente(string idPersona, string nombre, string apellido, string telefono, string email, string ciudad, string calle, string numero, string codigoPostal, string colonia, string idCliente)
        {
            this.idPersona = idPersona;
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Telefono = telefono;
            this.Email = email;
            this.Ciudad = ciudad;
            this.Calle = calle;
            this.Numero = numero;
            this.CodigoPostal = codigoPostal;
            this.Colonia = colonia;
            this.idCliente = idCliente;
        }
    }
}
