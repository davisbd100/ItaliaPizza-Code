using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;

namespace Controller
{
    public class InventarioController
    {

        public List<DataAccess.Inventario> ObtenerInventario()
        {
            List<DataAccess.Inventario> resultado = new List<DataAccess.Inventario>();
            InventarioDAO inventarioDAO = new InventarioDAO();
            resultado = inventarioDAO.ObtenerTodosLosInventarios();
            return resultado;
        }

        public ResultadoOperacionEnum.ResultadoOperacion ActualizarExistencias(List<DataAccess.Inventario> inventarios)
        {
            ResultadoOperacionEnum.ResultadoOperacion resultado = ResultadoOperacionEnum.ResultadoOperacion.FallaDesconocida;
            InventarioDAO inventarioDAO = new InventarioDAO();
            resultado = inventarioDAO.ActualizarInventario(inventarios);
            return resultado;
        }

        public ResultadoOperacionEnum.ResultadoOperacion ComprobarInventarioFinal(List<DataAccess.Inventario> inventarios)
        {
            ResultadoOperacionEnum.ResultadoOperacion resultado = ResultadoOperacionEnum.ResultadoOperacion.FallaDesconocida;
            InventarioDAO inventarioDAO = new InventarioDAO();
            List<DataAccess.Inventario> inventarioBD = inventarioDAO.ObtenerTodosLosInventarios();
            PedidoDAO pedidoDAO = new PedidoDAO();
            List<DataAccess.Pedido> pedidos = pedidoDAO.ObtenerPedidosPorFecha(DateTime.Now.AddDays(-1), DateTime.Now);
            foreach (var pedido in pedidos)
            {
                foreach (var pedidoProducto in pedido.PedidoProducto)
                {
                    for (int i = 0; i < pedidoProducto.Cantidad; i++)
                    {
                        if (pedidoProducto.ProductoVenta.TieneReceta == true)
                        {

                        }
                        else
                        {
                            foreach (var ingrediente in pedidoProducto.ProductoVenta.Receta1.RecetaIngrediente)
                            {
                            
                            }
                        }
                    }
                }
            }
            resultado = inventarioDAO.ActualizarInventario(inventarios);
            return resultado;
        }

    }
}
