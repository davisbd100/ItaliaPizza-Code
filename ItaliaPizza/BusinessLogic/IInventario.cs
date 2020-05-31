using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.ResultadoOperacionEnum;

namespace BusinessLogic
{
    interface IInventario
    {
        List<Inventario> ObtenerInventarios(Producto producto);
        ResultadoOperacion AddInventario(Inventario inventario);
        ResultadoOperacion ModificarInventario(Inventario inventario);
        int ObtenerPaginasDeTablaInventario(int elementosPorPagina);
        List<DataAccess.Inventario> ObtenerTodosLosInventarios();
        List<DataAccess.Inventario> ObtenerTodosLosInventariosConIngreso(int rango, int pagina);
        List<DataAccess.Inventario> ObtenerInventarioPorRango(int rango, int pagina);
        ResultadoOperacion ActualizarInventario(List<DataAccess.Inventario> inventarios);

    }
}
