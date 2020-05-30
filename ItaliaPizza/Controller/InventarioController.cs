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

        public ResultadoOperacionEnum.ResultadoOperacion CerrarDia()
        {
            ResultadoOperacionEnum.ResultadoOperacion resultado = ResultadoOperacionEnum.ResultadoOperacion.FallaDesconocida;
            PedidoDAO pedidoDAO = new PedidoDAO();
            List<DataAccess.Pedido> pedidos = pedidoDAO.ObtenerPedidosPorFecha((DateTime.Now.AddDays(-1)), DateTime.Now);
            DataAccess.DiaVenta diaVenta = new DataAccess.DiaVenta();
            diaVenta.Ingresos = 0;
            diaVenta.Fecha = DateTime.Now;
            foreach (var pedido in pedidos)
            {
                if (pedido.Estatus1.NombreEstatus == "Finalizado")
                {
                    foreach (var pedidoProducto in pedido.PedidoProducto)
                    {
                        diaVenta.Ingresos += pedidoProducto.Precio;
                    }
                    diaVenta.Pedido.Add(pedido);
                }
            }
            VentaDAO ventaDAO = new VentaDAO();
            resultado = ventaDAO.GuardarDiaVenta(diaVenta);

            return resultado;
        }

    }
}
