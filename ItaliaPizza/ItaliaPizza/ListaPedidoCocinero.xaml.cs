using BusinessLogic;
using Controller;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ItaliaPizza
{
    /// <summary>
    /// Lógica de interacción para Pedido.xaml
    /// </summary>
    public partial class ListaPedidoCocinero : Window
    {
        private class CustomPedidoProducto : DataAccess.PedidoProducto
        {
            public String NombreProducto { get; set; }
            public String CodigoProducto { get; set; }
        }
        public List<DataAccess.Pedido> pedidos { get; set; }
        public DataAccess.Pedido pedidoActual { get; set; }
        PedidoController PedidoController = new PedidoController();
        public ListaPedidoCocinero()
        {
            InitializeComponent();
            pedidos = PedidoController.ObtenerPedidosCocina();
        }

        private void PedidosUC_PedidoUserControlClicked(object sender, EventArgs e)
        {
            pedidoActual = ((DataAccess.Pedido)sender);
            lbidPedidoActual.Content = pedidoActual.idPedido;
            List<CustomPedidoProducto> custom = new List<CustomPedidoProducto>();
            foreach (var item in PedidoController.ObtenerPedidoProducto(pedidoActual.idPedido))
            {
                CustomPedidoProducto tempPedidoProducto = new CustomPedidoProducto
                {
                    idPedido = item.idPedido,
                    Cantidad = item.Cantidad,
                    Precio = item.Precio,
                    idProductoVenta = item.idProductoVenta
                };
                ProductoController productoController = new ProductoController();
                DataAccess.Producto producto = productoController.ObtenerProductoPorId(tempPedidoProducto.idProductoVenta);
                tempPedidoProducto.NombreProducto = producto.Nombre;
                tempPedidoProducto.CodigoProducto = producto.Codigo;
                custom.Add(tempPedidoProducto);
            }
            dgProductos.ItemsSource = custom;
            Console.WriteLine("hofbdjs");
        }

        private void btEnPreparacion_Click(object sender, RoutedEventArgs e)
        {
            if (pedidoActual == null)
            {
                MessageBox.Show("No se ha seleccionado un pedido!!!");
            }
            else if(pedidoActual.Estatus1.NombreEstatus == "En Preparación"){
                MessageBox.Show("No se puede seleccionar este pedido para ponerlo en preparacion por que ya se encuentra en preparacion");
            }
            else
            {
                try
                {
                    PedidoController.PonerEnPreparacion(pedidoActual.idPedido);
                    MessageBox.Show("Se ha puesto el pedido en preparacion");
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("No existen suficientes existencias en el inventario");
                }
                ucPedidos.UpdateGrid();
                pedidoActual = null;
                dgProductos.ItemsSource = null;
                lbidPedidoActual.Content = "Ninguno";

            }
        }

        private void btTerminado_Click(object sender, RoutedEventArgs e)
        {
            if (pedidoActual == null)
            {
                MessageBox.Show("No se ha seleccionado un pedido!!!");
            }
            else if (pedidoActual.Estatus1.NombreEstatus == "En Espera")
            {
                MessageBox.Show("Primero se debe preparar el pedido");
            }
            else
            {
                PedidoController.CambiarEstadoPedido(pedidoActual.idPedido, "Preparado");
                MessageBox.Show("Se ha terminado el pedido");
                ucPedidos.UpdateGrid();
                pedidoActual = null;
                dgProductos.ItemsSource = null;
                lbidPedidoActual.Content = "Ninguno";
            }
        }

        private void SalirButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
