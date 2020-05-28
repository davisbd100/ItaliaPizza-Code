using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.ResultadoOperacionEnum;


namespace BusinessLogic
{
    interface IProductoIngrediente
    {
        List<ProductoIngrediente> GetProductosIngrediente(int rango);
        List<ProductoIngrediente> ProdctoIngredienteBusqueda(string busqueda);
        ProductoIngrediente ObtenerProductoIngredientePorId(string codigo);
        ResultadoOperacion AddProductoIngrediente(ProductoIngrediente productoIngrediente, Inventario inventario);
        ResultadoOperacion EliminarProducto(int productoIngrediente);
        ResultadoOperacion EditarProducto(ProductoIngrediente productoIngrediente);

    }
}
