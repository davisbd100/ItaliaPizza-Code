using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Ubicación
    {
        public string Nombreubicación { get; set; }
        public Producto Producto { get; set; }
        public Double Cantidad { get; set; }

        public Ubicación() { }
    }
}
