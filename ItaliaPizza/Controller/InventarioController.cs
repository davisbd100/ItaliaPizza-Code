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
            PedidoDAO pedidoDAO = new PedidoDAO();
            //pedidoDAO.ObtenerPedidosPorFecha();
            resultado = inventarioDAO.ActualizarInventario(inventarios);
            return resultado;
        }

    }
}
