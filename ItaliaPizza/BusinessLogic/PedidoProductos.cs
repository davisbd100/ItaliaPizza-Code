using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class PedidoProductos
    {
        public int idPedido { get; set; }
        public int idProducto { get; set; }
        public double Cantidad { get; set; }
        public double Precio { get; set; }

        public PedidoProductos()
        {
            
        }
    }
}
