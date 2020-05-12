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
        ProductoVenta ObtenerProductoVentaPorid(string id);
        ResultadoOperacion AddProductoVenta(ProductoVenta productoVenta);
        ResultadoOperacion EliminarProductoVenta(ProductoVenta productoVenta);
        ResultadoOperacion EditarProductoVenta(ProductoVenta productoVenta);
    }
}
