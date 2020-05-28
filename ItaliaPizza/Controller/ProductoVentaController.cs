using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.ResultadoOperacionEnum;

namespace Controller
{
    public class ProductoVentaController
    {
        public ResultadoOperacion crearProducto(string nombre, int codigo, string descripcion, float precioUnitario,
            string restriccion, string unidadMedida, float precioVenta, bool requiereReceta, string foto, string ubicacion, int cantidad,
            string caducidad, string tipoProduct)
        {
            ResultadoOperacion resultadoOperacion = new ResultadoOperacion();
            Receta receta = new Receta();
            ProductoVentaDAO productoVentaDAO = new ProductoVentaDAO();
            InventarioDAO inventarioDAO = new InventarioDAO();
            TipoProducto tipoProducto = new TipoProducto();
            tipoProducto.NombreTipoProducto = tipoProduct;
            ProductoVenta productoVenta = new ProductoVenta();
            productoVenta.Código = codigo;
            productoVenta.Nombre = nombre;
            productoVenta.Descripción = descripcion;

            productoVenta.PrecioPúblico = precioVenta;
            productoVenta.Restricción = restriccion;
            productoVenta.TieneReceta = requiereReceta;
            productoVenta.TipoProducto = tipoProducto;
            productoVenta.FotoProducto = foto;
            productoVenta.Receta = receta;

            Inventario inventario = new Inventario();
            // inventario.Caducidad = DateTime.Parse(caducidad);
            inventario.idInventario = codigo;
            inventario.Caducidad = caducidad;
            inventario.CantidadIngreso = cantidad;
            inventario.PrecioCompra = precioUnitario;
            inventario.Producto = productoVenta;
            inventario.ExistenciaTotal = cantidad;
            inventario.UnidadDeMedida = unidadMedida;
            inventario.FechaIngreso = DateTime.Now;


            resultadoOperacion = productoVentaDAO.AddProductoVenta(productoVenta, inventario);

            if (resultadoOperacion == ResultadoOperacionEnum.ResultadoOperacion.Exito)
            {
                //inventarioDAO.AddInventario(inventario);
            }

            return resultadoOperacion;
        }


        public List<ProductoVenta> ObtenerProductoVenta(int rango)
        {
            const int NUM_RESULTADOS = 19;
            rango -= 1;
            rango *= NUM_RESULTADOS;
            ProductoVentaDAO productoVentaDAO = new ProductoVentaDAO();
            List < ProductoVenta > productoVentas = productoVentaDAO.GetProductosVenta(rango);
            return productoVentas;
        }


        public ProductoVenta BuscarProductoVenta(int id)
        {
            ProductoVentaDAO productoVentaDAO = new ProductoVentaDAO();

            ProductoVenta productoVenta = productoVentaDAO.ObtenerProductoVentaPorid(id);
            return productoVenta;
 
        }

        public ResultadoOperacion EliminarproductoVenta(Producto producto)
        {
            ProductoVentaDAO productoVentaDAO = new ProductoVentaDAO();
            ResultadoOperacion resultado = productoVentaDAO.EliminarProductoVenta(producto.Código);
            return resultado;
        }
    }
}
