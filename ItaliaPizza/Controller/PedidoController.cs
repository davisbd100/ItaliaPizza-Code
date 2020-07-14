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
        public ResultadoOperacionEnum.ResultadoOperacion CambiarEstadoPedido(int id, String Estado)
        {
            ResultadoOperacionEnum.ResultadoOperacion resultado;
            PedidoDAO pedidoDAO = new PedidoDAO();
            BusinessLogic.Pedido tempPedido = new BusinessLogic.Pedido()
            {
                idPedido = id
            };
            resultado = pedidoDAO.CambiarEstadoPedido(tempPedido, pedidoDAO.ObtenerEstatusPorNombre(Estado));
            return resultado;
        }

        public List<DataAccess.Pedido> ObtenerPedidosCocina()
        {
            List<DataAccess.Pedido> resultado;
            PedidoDAO pedidoDAO = new PedidoDAO();
            resultado = pedidoDAO.ObtenerListaPedidosDisponiblesCocina();
            return resultado;
        }
        public List<DataAccess.PedidoProducto> ObtenerPedidoProducto(int id)
        {
            List<DataAccess.PedidoProducto> resultado;
            PedidoDAO pedidoDAO = new PedidoDAO();
            resultado = pedidoDAO.ObtenerListaPedidoProducto(id);
            return resultado;
        }

        public List<DataAccess.Pedido> ObtenerPedidosVendedor()
        {
            List<DataAccess.Pedido> aidList;
            PedidoDAO pedidoDAO = new PedidoDAO();
            aidList = pedidoDAO.ObtenerListaPedidosDisponibles();
            List<DataAccess.Pedido> resultado = aidList.ToList();
            if (!aidList.Any())
            {
                throw new DataException("No existen pedidos");
            }
            return resultado;
        }

        public DataAccess.Pedido ObtenerPedidoConProductos(int idPedido)
        {
            DataAccess.Pedido resultado;
            PedidoDAO pedidoDAO = new PedidoDAO();
            resultado = pedidoDAO.GetPedidoConProductoPorId(idPedido);
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


        public ResultadoOperacionEnum.ResultadoOperacion crearPedidoMesero(DataAccess.Pedido pedido, List<DataAccess.PedidoProducto> productos)
        {
            ResultadoOperacionEnum.ResultadoOperacion resultado = new ResultadoOperacionEnum.ResultadoOperacion();
            PedidoDAO pedidoDAO = new PedidoDAO();
            resultado = pedidoDAO.CrearPedidoMesero(pedido, productos);

            return resultado;
        }
    }
}
