using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Microsoft.SqlServer.Server;
using DataAccess;

namespace Controller
{
    public class PedidoController
    {

        public ResultadoOperacionEnum.ResultadoOperacion CancelarPedido(BusinessLogic.Pedido pedido)
        {
            ResultadoOperacionEnum.ResultadoOperacion resultado = ResultadoOperacionEnum.ResultadoOperacion.FallaDesconocida;
            PedidoDAO pedidoDAO = new PedidoDAO();
            resultado = pedidoDAO.CambiarEstadoPedido(pedido, pedidoDAO.ObtenerEstatusPorNombre("Cancelado"));
            return resultado;
        }

        public List<DataAccess.Pedido> obtenerPedidosCocina()
        {
            List<DataAccess.Pedido> resultado;
            PedidoDAO pedidoDAO = new PedidoDAO();
            resultado = pedidoDAO.ObtenerListaPedidos();
            foreach (var item in resultado)
            {
                if (item.Estatus1.NombreEstatus != "En espera" || item.Estatus1.NombreEstatus != "En Preparación")
                {
                    resultado.Remove(item);
                }
            }
            return resultado;
        }

        public List<DataAccess.Pedido> obtenerPedidosCajero()
        {
            List<DataAccess.Pedido> resultado;
            PedidoDAO pedidoDAO = new PedidoDAO();
            resultado = pedidoDAO.ObtenerListaPedidos();
            return resultado;
        }
    }
}
