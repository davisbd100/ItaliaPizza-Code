using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    interface IPedido
    {
        Pedido GetPedidoPorId(int id);
        ResultadoOperacionEnum.ResultadoOperacion CambiarEstadoPedido(Pedido pedido, DataAccess.Estatus estatus);
        ResultadoOperacionEnum.ResultadoOperacion CambiarPedido(Pedido pedido);
        ResultadoOperacionEnum.ResultadoOperacion CambiarProductosDePedido(int idPedido, List<ProductoVenta> productos);
        DataAccess.Estatus ObtenerEstatusPorNombre(String estatus);
    }
}
