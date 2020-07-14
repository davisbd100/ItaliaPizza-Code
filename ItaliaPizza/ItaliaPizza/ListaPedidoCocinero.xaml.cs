using Controller;
using System;
using System.Collections.Generic;
using System.Windows;

namespace PrototiposItaliaPizza
{
    /// <summary>
    /// Lógica de interacción para Pedido.xaml
    /// </summary>
    public partial class ListaPedidoCocinero : Window
    {
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
            DataAccess.Pedido pedido = PedidoController.ObtenerPedidoConProductos(pedidoActual.idPedido);
            dgProductos.ItemsSource = pedido.PedidoProducto;
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
                PedidoController.CambiarEstadoPedido(pedidoActual.idPedido, "En Preparación");
                MessageBox.Show("Se ha puesto el pedido en preparacion");
                ucPedidos.UpdateGrid();
                pedidoActual = null;
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
                lbidPedidoActual.Content = "Ninguno";
            }
        }

        private void SalirButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
