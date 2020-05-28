using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    interface IProducto
    {
        List<Producto> getProductos(int rango);
        List<Producto> BuscarProducto(string busqueda);
    }
}
