using BusinessLogic;
using Controller;
using DataAccess;
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
        public List<BusinessLogic.ProductoVenta> productoVentas = new List<BusinessLogic.ProductoVenta>();
        List<DataAccess.PedidoProducto> listaproductos = new List<PedidoProducto>();

        public PedidoCallCenter()
        {
            InitializeComponent();
            ActualizarClientes();
        }

        private void ActualizarClientes()
        {
            ClienteController clienteController = new ClienteController();
            List<BusinessLogic.Cliente> clientes = new List<BusinessLogic.Cliente>();
            BusinessLogic.Cliente cliente = new BusinessLogic.Cliente();
            cliente.Nombre = "arturo";
            cliente.idCliente = "12";
            clientes.Add(cliente);
           // List<Cliente> clientes = clienteController.GetCliente(0);
            cbb_NombreCliente.ItemsSource = clientes;
        }

        private void ProductosUC_ProductoUserControlClicked(object sender, EventArgs e)
        {
            BusinessLogic.ProductoVenta tempProducto = ((BusinessLogic.ProductoVenta)sender);
            ProductoVentaController producto = new ProductoVentaController();
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

        private bool ValidarCampos()
        {
            if ((BusinessLogic.Cliente)cbb_NombreCliente.SelectedItem == null)
            {
                MessageBox.Show("Debes elegir un cliente");
                return false;
            }
            return true;
        }

        private void btn_NuevoPedido_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos())
            {
                DataAccess.Pedido pedido = new DataAccess.Pedido();
                pedido.FechaPedido = DateTime.UtcNow;
                pedido.Estatus = 1;
                BusinessLogic.Cliente cliente = (BusinessLogic.Cliente)cbb_NombreCliente.SelectedItem;
                pedido.Cliente = cliente.idCliente;


                foreach (BusinessLogic.ProductoVenta producto in productoVentas)
                {
                    DataAccess.PedidoProducto pedidoProducto = new PedidoProducto();
                    pedidoProducto.idProductoVenta = producto.idProducto;
                    pedidoProducto.Precio = producto.PrecioPúblico;
                    if (listaproductos != null)
                    {
                        foreach (DataAccess.PedidoProducto pedido1 in listaproductos)
                        {
                            if (pedido1.idPedido == producto.idProducto)
                            {

                                pedido1.Cantidad += 1;
                            }
                            else
                            {
                                pedidoProducto.Cantidad = 1;

                            }
                        }

                    }
                        listaproductos.Add(pedidoProducto);

                }


                foreach (DataAccess.PedidoProducto pedido2 in listaproductos)
                {
                    MessageBox.Show(pedido2.Cantidad.ToString());
                }
            }
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
