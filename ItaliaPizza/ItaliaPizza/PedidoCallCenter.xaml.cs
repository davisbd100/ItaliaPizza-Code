using BusinessLogic;
using Controller;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for PedidoCallCenter.xaml
    /// </summary>
    public partial class PedidoCallCenter : Window
    {

        public DataAccess.Pedido PedidoAEditar;
        double Total = 0;
        public List<ProductoVenta> productoVentas = new List<ProductoVenta>();

        public PedidoCallCenter()
        {
            InitializeComponent();
            ActualizarClientes();
        }

        private void ActualizarClientes()
        {
            ClienteController clienteController = new ClienteController();
            List<Cliente> clientes = new List<Cliente>();
            Cliente cliente = new Cliente();
            cliente.Nombre = "arturo";
            clientes.Add(cliente);
           // List<Cliente> clientes = clienteController.GetCliente(0);
            cbb_NombreCliente.ItemsSource = clientes;
        }

        private void ProductosUC_ProductoUserControlClicked(object sender, EventArgs e)
        {
            ProductoVenta tempProducto = ((ProductoVenta)sender);
            ProductoVentaController producto = new ProductoVentaController();
            int cantidad = 1;

            productoVentas.Add(tempProducto);
            ActualizarLabelPrecio(tempProducto.PrecioPúblico);
            ActualizarDataGrid();
        }

        void ActualizarLabelPrecio(double PrecioAAgregar)
        {

            Total += PrecioAAgregar;
            lbNuevoPrecio.Text = String.Format("{0:0.00}", Total) + "  MXN";
        }

        private void ActualizarDataGrid()
        {
            dgProductosDePedido.ItemsSource = null;
            dgProductosDePedido.ItemsSource = productoVentas;
        }

        private void btn_NuevoPedido_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_NuevoCliente_Click(object sender, RoutedEventArgs e)
        {
            RegistrarCliente registrarCliente = new RegistrarCliente();
            registrarCliente.Show();
        }

        private void btn_Actualizar_Click(object sender, RoutedEventArgs e)
        {
            ActualizarClientes();
        }
    }
}
