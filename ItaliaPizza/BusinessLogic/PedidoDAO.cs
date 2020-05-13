using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    class PedidoDAO : IPedido
    {
        public ResultadoOperacionEnum.ResultadoOperacion CambiarEstadoPedido(Pedido pedido, Estatus estatus)
        {
            throw new NotImplementedException();
        }

        public ResultadoOperacionEnum.ResultadoOperacion CambiarPedido(Pedido pedido)
        {
            throw new NotImplementedException();
        }

        public Pedido GetPedidoPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
