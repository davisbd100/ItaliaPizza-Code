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
            List<DataAccess.Pedido> aidList;
            PedidoDAO pedidoDAO = new PedidoDAO();
            aidList = pedidoDAO.ObtenerListaPedidosDisponibles();
            List<DataAccess.Pedido> resultado = aidList.ToList();
            if (aidList.Any())
            {
                foreach (var item in aidList)
                {
                    if (item.Estatus1.NombreEstatus != "En Espera" && item.Estatus1.NombreEstatus != "En Preparación")
                    {
                        resultado.Remove(item);
                    }
                }
            }
            else
            {
                throw new DataException("No existen pedidos");
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
    }
}
