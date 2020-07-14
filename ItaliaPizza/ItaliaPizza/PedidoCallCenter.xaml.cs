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
        List<BusinessLogic.Cliente> clientes = new List<BusinessLogic.Cliente>();

        public PedidoCallCenter()
        {
            InitializeComponent();
            ActualizarClientes();
        }

        private void ActualizarClientes()
        {
            ClienteController clienteController = new ClienteController();
            //List<BusinessLogic.Cliente> clientes = new List<BusinessLogic.Cliente>();
            //BusinessLogic.Cliente cliente = new BusinessLogic.Cliente();
            //cliente.Nombre = "arturo";
            //cliente.idCliente = "12";
            //clientes.Add(cliente);
            clientes = clienteController.GetCliente(1);
            cbb_NombreCliente.ItemsSource = clientes;
        }

        private void ProductosUC_ProductoUserControlClicked(object sender, EventArgs e)
        {
            BusinessLogic.ProductoVenta tempProducto = ((BusinessLogic.ProductoVenta)sender);
            DataAccess.ProductoVenta tempProducto1 = new DataAccess.ProductoVenta()
            {
                idProductoVenta = tempProducto.idProducto,
                PrecioPublico = tempProducto.PrecioPúblico,
                FotoProducto = tempProducto.Nombre
            };
            ProductoVentaController producto = new ProductoVentaController();
            DataAccess.PedidoProducto pedidoProducto = new DataAccess.PedidoProducto()
            {
                idProductoVenta = tempProducto.idProducto,
                Cantidad = 1,
                Precio = tempProducto.PrecioPúblico
            };
            foreach (DataAccess.PedidoProducto item in dgProductosDePedido.Items)
            {
                if (tempProducto.idProducto == item.idProductoVenta)
                {
                    item.Cantidad++;
                    item.Precio += tempProducto1.PrecioPublico;
                }
            }
            int flag = 0;
            foreach (DataAccess.PedidoProducto item in dgProductosDePedido.Items)
            {
                if (item.ProductoVenta.idProductoVenta == tempProducto.idProducto)
                {
                    flag = 1;
                    break;
                }
            }
            if (flag == 0)
            {
                pedidoProducto.ProductoVenta = tempProducto1;

                listaproductos.Add(pedidoProducto);
                ActualizarDataGrid();
            }

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
            dgProductosDePedido.ItemsSource = listaproductos;
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
                BusinessLogic.Cliente cliente1 = clientes[cbb_NombreCliente.SelectedIndex];
                //string cliente = cbb_NombreCliente.SelectedIndex;
                MessageBox.Show(cliente1.idPersona);
                pedido.Cliente = cliente1.idPersona;

                foreach (BusinessLogic.ProductoVenta producto in productoVentas)
                {
                    if (dgProductosDePedido.Items != null)
                    {
                        foreach (BusinessLogic.ProductoVenta pedido1 in dgProductosDePedido.Items)
                        {
                            DataAccess.PedidoProducto pedidoProducto = new PedidoProducto();
                            pedidoProducto.idProductoVenta = pedido1.idProducto;
                            pedidoProducto.Precio = pedido1.PrecioPúblico;
                            listaproductos.Add(pedidoProducto);
                        }
                    }

                }

                PedidoController pedidoController = new PedidoController();
                if(pedidoController.crearPedidoDomicilio(pedido, listaproductos) == ResultadoOperacionEnum.ResultadoOperacion.Exito)
                {
                    MessageBox.Show("El Pedido se registró correctamente");
                }
                else
                {
                    MessageBox.Show("No se puede registar el Pedido");
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