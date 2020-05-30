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
        List<DataAccess.Inventario> ObtenerTodosLosInventarios();
        List<DataAccess.Inventario> ObtenerTodosLosInventariosConIngreso();
        ResultadoOperacion ActualizarInventario(List<DataAccess.Inventario> inventarios);

    }
}
