using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Persona
    {
        public int idPersona { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public String Telefono { get; set; }
        public String Email { get; set; }
        public String Calle { get; set; }
        public String Numero { get; set; }
        public String CodigoPostal { get; set; }
        public String Colonia { get; set; }
        public String Ciudad { get; set; }

        public Persona()
        {

        }
    }
}