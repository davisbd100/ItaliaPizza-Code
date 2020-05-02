﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.ResultadoOperacionEnum;


namespace BusinessLogic
{
    interface IProductoIngrediente
    {
        List<ProductoIngrediente> GetProductosIngrediente();
        List<ProductoIngrediente> ProdctoIngredienteBusqueda();
        ProductoIngrediente ObtenerProductoIngredientePorId(string id);
        ResultadoOperacion AddProductoIngrediente(ProductoIngrediente productoIngrediente);
        ResultadoOperacion EliminarProducto(ProductoIngrediente productoIngrediente);
        ResultadoOperacion EditarProducto(ProductoIngrediente productoIngrediente);

    }
}
