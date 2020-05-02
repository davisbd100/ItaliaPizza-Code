using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.ResultEnum;


namespace BusinessLogic
{
    interface IProductoIngrediente
    {
        List<ProductoIngrediente> GetProductoIngredientes();
        ResultOperation AddProductoIngrediente(ProductoIngrediente productoIngrediente);
        ResultOperation EliminarProducto(ProductoIngrediente productoIngrediente);

    }
}
