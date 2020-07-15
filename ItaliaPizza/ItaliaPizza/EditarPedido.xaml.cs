using BusinessLogic;
using Controller;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ItaliaPizza
{
    /// <summary>
    /// Lógica de interacción para EditarCliente.xaml
    /// </summary>
    public partial class EditarPedido : Window
    {
        private class CustomPedidoProducto: DataAccess.PedidoProducto
        {
            public String NombreProducto { get; set; }
        }

        int PedidoID { get; set; }
        public DataAccess.Pedido PedidoAEditar;
        List<CustomPedidoProducto> custom = new List<CustomPedidoProducto>();
        PedidoController controller = new PedidoController();
        public EditarPedido()
        {
            InitializeComponent();
            PedidoID = 1;
            ObtenerPedido();
        }
        public EditarPedido(int id)
        {
            InitializeComponent();
            PedidoID = id;
            ObtenerPedido();
        }

        void ObtenerPedido()
        {
            try
            {
                PedidoAEditar = controller.ObtenerPedidoParaEditar(PedidoID);
            }
            catch (EntityException)
            {
                MessageBox.Show("No se pudo obtener el pedido para la edicion \nReintentar mas tarde");
                this.Close();
            }
            catch (InstanceNotFoundException)
            {
                MessageBox.Show("No se encontro el pedido, verificar la existencia");
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("El pedido no esta en espera y ya no es posible modificarlo");
                this.Close();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ActualizarDataGrid();
            lbEstatus.Content = PedidoAEditar.Estatus1.NombreEstatus;
            lbIdCliente.Content = PedidoAEditar.Cliente;
            lbIdPedido.Content = PedidoAEditar.idPedido;
            double CostoTotal = 0;
            lbNuevoPrecio.Content = String.Format("{0:0.00}", CostoTotal) + "  MXN";


            foreach (var pedidoProducto in PedidoAEditar.PedidoProducto)
            {
                CostoTotal += (double)pedidoProducto.Precio;
            }
            lbPrecioAnterior.Content = String.Format("{0:0.00}", CostoTotal) + "  MXN";
            lbNuevoPrecio.Content = String.Format("{0:0.00}", CostoTotal) + "  MXN";
        }

        private void ActualizarDataGrid()
        {
            dgProductosDePedido.ItemsSource = null;
            foreach (var item in PedidoAEditar.PedidoProducto)
            {
                CustomPedidoProducto productoVenta = new CustomPedidoProducto()
                {
                    Cantidad = item.Cantidad,
                    idPedido = item.idPedido,
                    idProductoVenta = item.idProductoVenta,
                    Precio = item.Precio,
                    ProductoVenta = item.ProductoVenta
                };
                ProductoController productoController = new ProductoController();
                productoVenta.NombreProducto = productoController.ObtenerProductoPorId(productoVenta.idProductoVenta).Nombre;
                custom.Add(productoVenta);
            }
            dgProductosDePedido.ItemsSource = custom;
        }

        private void Quitar_Click(object sender, RoutedEventArgs e)
        {
            if (dgProductosDePedido.SelectedIndex != -1)
            {
                DataAccess.PedidoProducto tempPedidoProducto = (DataAccess.PedidoProducto)dgProductosDePedido.SelectedItem;
                ActualizarLabelPrecio(-((double)tempPedidoProducto.Precio));
                PedidoAEditar.PedidoProducto.Remove(tempPedidoProducto);
                ActualizarDataGrid();
            }
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btEditar_Click(object sender, RoutedEventArgs e)
        {
            if (PedidoAEditar.PedidoProducto.Any())
            {
                GuardarEdicionPedido();
            }
            else
            {
                MessageBox.Show("No se puede guardar un pedido sin productos");
            }
        }

        private void GuardarEdicionPedido()
        {
            switch(controller.CambiarProductosPedido(PedidoAEditar.idPedido, custom.ConvertAll(b => (PedidoProducto)b)))
            {
                case (ResultadoOperacionEnum.ResultadoOperacion.Exito):
                    MessageBox.Show("Guardado con exito");
                    this.Close();
                    break;
                case (ResultadoOperacionEnum.ResultadoOperacion.FallaDesconocida):
                    MessageBox.Show("Fallo no esperado, reintentar mas tarde");
                    break;
                case (ResultadoOperacionEnum.ResultadoOperacion.FalloSQL):
                    MessageBox.Show("Error al conectar con la base de datos, reintentar mas tarde");
                    break;
            }
        }

        private void ProductosUC_ProductoUserControlClicked(object sender, EventArgs e)
        {
            DataAccess.ProductoVenta tempProducto = ((DataAccess.ProductoVenta)sender);
            ProductoVentaController producto = new ProductoVentaController();
            int cantidad = 1;
            DataAccess.PedidoProducto tempPedidoProducto = new DataAccess.PedidoProducto()
            {
                Cantidad = cantidad,
                idPedido = PedidoAEditar.idPedido
                //ProductoVenta = producto.ObtenerProductoPorIdEE(int.Parse(tempProducto.Código)),
                //Precio = cantidad * tempProducto.PrecioPúblico
            };
            foreach (DataAccess.PedidoProducto item in dgProductosDePedido.Items)
            {
                if (item.ProductoVenta.idProductoVenta == tempPedidoProducto.ProductoVenta.idProductoVenta)
                {
                    item.Cantidad++;
                    item.Precio += tempPedidoProducto.ProductoVenta.PrecioPublico;
                    ActualizarLabelPrecio((double)tempPedidoProducto.Precio);
                    ActualizarDataGrid();
                    return;
                }
            }
            ActualizarLabelPrecio((double)tempPedidoProducto.Precio);
            PedidoAEditar.PedidoProducto.Add(tempPedidoProducto);
            ActualizarDataGrid();
        }

        void ActualizarLabelPrecio(double PrecioAAgregar)
        {
            double oldNuevoPrecio = ObtenerPrecioDouble(lbNuevoPrecio.Content.ToString());
            double nuevoPrecio = oldNuevoPrecio + PrecioAAgregar;
            lbNuevoPrecio.Content = String.Format("{0:0.00}", nuevoPrecio) + "  MXN";
        }


        double ObtenerPrecioDouble(String precio)
        {
            var match = Regex.Match(precio, @"([-+]?[0-9]*\.?[0-9]+)");
            double resultado = -1;
            if (match.Success)
            {
                resultado = Convert.ToDouble(match.Groups[1].Value);
            }
            else
            {
                Console.WriteLine("No numeros encontrados");
            }
            return resultado;
        }
    }
}
