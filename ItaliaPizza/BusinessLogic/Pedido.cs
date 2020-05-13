using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    class Pedido
    {
        public int idPedido { get; set; }
        public DateTime FechaPedido { get; set; }
        public int NumeroMesa { get; set; }
        public int Estatus { get; set; }
        public String Comentario { get; set; }
        public DateTime HoraSalida { get; set; }
        public String Repartidor { get; set; }
        public Cliente Cliente { get; set; }
        public Empleado Empleado { get; set; }

        public Pedido()
        {

        }
    }
}
