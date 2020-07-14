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
        DataAccess.Pedido GetPedidoConProductoPorId(int id);
        ResultadoOperacionEnum.ResultadoOperacion CrearPedidoDomicilio(DataAccess.Pedido pedido, List<DataAccess.PedidoProducto> productos);
        ResultadoOperacionEnum.ResultadoOperacion CambiarEstadoPedido(Pedido pedido, DataAccess.Estatus estatus);
        ResultadoOperacionEnum.ResultadoOperacion CambiarPedido(Pedido pedido);
        ResultadoOperacionEnum.ResultadoOperacion CambiarProductosDePedido(int idPedido, ICollection<DataAccess.PedidoProducto> productos);
        int ObtenerPaginasDeTablaPedido(int elementosPorPagina);
        DataAccess.Estatus ObtenerEstatusPorNombre(String estatus);
        List<DataAccess.Pedido> ObtenerListaPedidos();
        List<DataAccess.Pedido> ObtenerPedidosPorFecha(DateTime inicial, DateTime final);
        List<DataAccess.Pedido> ObtenerPedidosPorRangoCocinero(int rango, int pagina);
    }
}
