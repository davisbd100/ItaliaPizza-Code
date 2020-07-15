using Controller;
using ItaliaPizza;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Lógica de interacción para Pedido.xaml
    /// </summary>
    public partial class ListaPedidoCallCenter : Window
    {
        private class CustomPedidoProducto : DataAccess.PedidoProducto
        {
            public String NombreProducto { get; set; }
            public double PrecioPublico { get; set; }
        }
        public List<DataAccess.Pedido> pedidos { get; set; }
        public DataAccess.Pedido pedidoActual { get; set; }
        PedidoController PedidoController = new PedidoController();

        public ListaPedidoCallCenter()
        {
            InitializeComponent();
            pedidos = PedidoController.ObtenerPedidosVendedor();
        }

        private void PedidosUC_PedidoUserControlClicked(object sender, EventArgs e)
        {
            pedidoActual = ((DataAccess.Pedido)sender);
            lbidPedidoActual.Content = pedidoActual.idPedido;
            List<CustomPedidoProducto> custom = new List<CustomPedidoProducto>();
            double subTotal = 0;
            foreach (var item in PedidoController.ObtenerPedidoProducto(pedidoActual.idPedido))
            {
                CustomPedidoProducto tempPedidoProducto = new CustomPedidoProducto
                {
                    idPedido = item.idPedido,
                    Cantidad = item.Cantidad,
                    Precio = item.Precio,
                    idProductoVenta = item.idProductoVenta
                };
                subTotal += (double)tempPedidoProducto.Precio;
                ProductoController productoController = new ProductoController();
                DataAccess.Producto producto = productoController.ObtenerProductoPorId(tempPedidoProducto.idProductoVenta);
                ProductoVentaController productoVentaController = new ProductoVentaController();
                DataAccess.ProductoVenta productoVenta = productoVentaController.ObtenerProductoPorIdEE(tempPedidoProducto.idProductoVenta);
                tempPedidoProducto.NombreProducto = producto.Nombre;
                tempPedidoProducto.PrecioPublico = (double)productoVenta.PrecioPublico;
                custom.Add(tempPedidoProducto);
            }
            tbSubtotal.Text = "$" + subTotal.ToString();
            double iva = Math.Round((subTotal / 100) * 16, 3);
            tbIva.Text = "$" + iva.ToString();
            tbTotal.Text = "$" + Math.Round(subTotal + iva, 3).ToString();
            dgProductos.ItemsSource = custom;
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            if (pedidoActual == null)
            {
                MessageBox.Show("No se ha seleccionado un pedido!!!");
            }
            else if (pedidoActual.Estatus1.NombreEstatus == "En Preparación")
            {
                MessageBox.Show("El pedido entro en preparación, por lo que ya no se puede cancelar");
            }
            else if (pedidoActual.Estatus1.NombreEstatus != "En Espera")
            {
                MessageBox.Show("El pedido solo se puede cancelar en espera de preparación");
            }
            else
            {
                CancelarPedido cancelar = new CancelarPedido();
                cancelar.ShowDialog();
                ucPedidos.UpdateGrid();
                pedidoActual = null;
                dgProductos.ItemsSource = null;
                lbidPedidoActual.Content = "Ninguno";
            }
        }

        private void btEditar_Click(object sender, RoutedEventArgs e)
        {
            if (pedidoActual == null)
            {
                MessageBox.Show("No se ha seleccionado un pedido!!!");
            }
            else if (pedidoActual.Estatus1.NombreEstatus == "En Preparación")
            {
                MessageBox.Show("El pedido entro en preparación, por lo que ya no se puede editar");
            }
            else if (pedidoActual.Estatus1.NombreEstatus != "En Espera")
            {
                MessageBox.Show("El pedido solo se puede editar en espera de preparación");
            }
            else
            {
                EditarPedido editar = new EditarPedido(pedidoActual.idPedido);
                editar.ShowDialog();
                ucPedidos.UpdateGrid();
                pedidoActual = null;
                dgProductos.ItemsSource = null;
                lbidPedidoActual.Content = "Ninguno";
            }
        }

        private void btPagar_Click(object sender, RoutedEventArgs e)
        {
            if (pedidoActual == null)
            {
                MessageBox.Show("No se ha seleccionado un pedido!!!");
            }
            else if (pedidoActual.Estatus1.NombreEstatus != "Entregado")
            {
                MessageBox.Show("El pedido aun no ha sido entregado!");
            }else
            {
                PedidoController.CambiarEstadoPedido(pedidoActual.idPedido, "Finalizado");
                MessageBox.Show("Se ha pagado el pedido");
                ucPedidos.UpdateGrid();
                pedidoActual = null;
                dgProductos.ItemsSource = null;
                lbidPedidoActual.Content = "Ninguno";
            }
        }

        private void btCerrarDia_Click(object sender, RoutedEventArgs e)
        {
            if (!pedidos.Any())
            {
                InventarioController inventario = new InventarioController();
                inventario.CerrarDia();
                MessageBox.Show("Se cerro el dia!");
            }
            else
            {
                MessageBox.Show("No se puede cerrar el dia con pedidos pendientes!!!");
            }
        }

        private void btEnCamino_Click(object sender, RoutedEventArgs e)
        {
            if (pedidoActual == null)
            {
                MessageBox.Show("No se ha seleccionado un pedido!!!");
            }
            else if (pedidoActual.Estatus1.NombreEstatus != "Preparado")
            {
                MessageBox.Show("El pedido se debe encontrar preparado antes de enviarlo");
            }
            else
            {
                PedidoController.CambiarEstadoPedido(pedidoActual.idPedido, "En Camino");
                MessageBox.Show("El pedido se encuentra en camino");
                ucPedidos.UpdateGrid();
                pedidoActual = null;
                dgProductos.ItemsSource = null;
                lbidPedidoActual.Content = "Ninguno";
            }

        }

        private void btEntregado_Click(object sender, RoutedEventArgs e)
        {
            if (pedidoActual == null)
            {
                MessageBox.Show("No se ha seleccionado un pedido!!!");
            }
            else if (pedidoActual.Estatus1.NombreEstatus != "En Camino")
            {
                MessageBox.Show("El pedido no ha sido marcado como enviado");
            }
            else
            {
                PedidoController.CambiarEstadoPedido(pedidoActual.idPedido, "Entregado");
                MessageBox.Show("Se ha Entregado el pedido, en espera del pago");
                ucPedidos.UpdateGrid();
                pedidoActual = null;
                dgProductos.ItemsSource = null;
                lbidPedidoActual.Content = "Ninguno";
            }
        }
    }
}
