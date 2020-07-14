using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Microsoft.SqlServer.Server;
using DataAccess;
using System.Data;
using System.Management.Instrumentation;

namespace Controller
{
    public class PedidoController
    {

        public ResultadoOperacionEnum.ResultadoOperacion CancelarPedido(BusinessLogic.Pedido pedido)
        {
            ResultadoOperacionEnum.ResultadoOperacion resultado;
            PedidoDAO pedidoDAO = new PedidoDAO();
            resultado = pedidoDAO.CambiarEstadoPedido(pedido, pedidoDAO.ObtenerEstatusPorNombre("Cancelado"));
            return resultado;
        }

        public List<DataAccess.Pedido> ObtenerPedidosCocina()
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

        public List<DataAccess.Pedido> ObtenerPedidosVendedor()
        {
            List<DataAccess.Pedido> resultado;
            PedidoDAO pedidoDAO = new PedidoDAO();
            resultado = pedidoDAO.ObtenerListaPedidos();
            return resultado;
        }

        public DataAccess.Pedido ObtenerPedidoParaEditar(int idPedido)
        {
            DataAccess.Pedido pedido;
            PedidoDAO pedidoDAO = new PedidoDAO();
            try
            {
                pedido = pedidoDAO.GetPedidoConProductoPorId(idPedido);
            }
            catch (EntityException entityException)
            {
                throw entityException;
            } catch (InstanceNotFoundException notFound)
            {
                throw notFound;
            } catch (FormatException formato)
            {
                throw formato;
            }
            return pedido;
        }

        public ResultadoOperacionEnum.ResultadoOperacion CambiarProductosPedido(int idPedido, ICollection<DataAccess.PedidoProducto> productos)
        {
            ResultadoOperacionEnum.ResultadoOperacion resultado;
            PedidoDAO pedidoDAO = new PedidoDAO();
            resultado = pedidoDAO.CambiarProductosDePedido(idPedido, productos);
            return resultado;
        }

        public ResultadoOperacionEnum.ResultadoOperacion crearPedidoDomicilio(DataAccess.Pedido pedido, List<DataAccess.PedidoProducto> productos)
        {
            ResultadoOperacionEnum.ResultadoOperacion resultado = new ResultadoOperacionEnum.ResultadoOperacion();
            PedidoDAO pedidoDAO = new PedidoDAO();
            resultado = pedidoDAO.CrearPedidoDomicilio(pedido, productos);

            return resultado;
        }
    }
}
