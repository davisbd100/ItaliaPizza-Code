using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class ProductoController
    {


        public List<Producto> ObtenerProducto(int rango)
        {
            const int NUM_RESULTADOS = 19;
            rango -= 1;
            rango *= NUM_RESULTADOS;

            ProductoDAO productoDAO = new ProductoDAO();
            List<Producto> productos = productoDAO.getProductos(rango);
            return productos;
        }
    }
}
