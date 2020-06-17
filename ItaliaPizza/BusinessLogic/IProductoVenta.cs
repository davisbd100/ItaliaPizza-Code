using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.ResultadoOperacionEnum;


namespace BusinessLogic
{
    interface IProductoVenta
    {
        List<ProductoVenta> GetProductosVenta(int rango);
        List<ProductoVenta> ProductoVentaBusqueda(string busqueda);
        List<ProductoVenta> ProductoVentaBusquedaRango(int rango, string busqueda);
        int ObtenerPaginasDeTablaProductoVenta();
        ProductoVenta ObtenerProductoVentaPorid(int codigo);
        ResultadoOperacion AddProductoVenta(ProductoVenta productoVenta, Inventario inventario);
        ResultadoOperacion EliminarProductoVenta(int productoVenta);
        ResultadoOperacion EditarProductoVenta(ProductoVenta productoVenta);
    }
}
