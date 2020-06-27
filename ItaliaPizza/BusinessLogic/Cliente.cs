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
    }
}
