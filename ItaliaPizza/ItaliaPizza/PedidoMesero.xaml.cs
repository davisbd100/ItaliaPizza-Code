using BusinessLogic;
using Controller;
using DataAccess;
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
    /// Interaction logic for PedidoMesero.xaml
    /// </summary>
    public partial class PedidoMesero : Window
    {

        public DataAccess.Pedido PedidoAEditar;
        double Total = 0;
        public List<BusinessLogic.ProductoVenta> productoVentas = new List<BusinessLogic.ProductoVenta>();
        List<DataAccess.PedidoProducto> listaproductos = new List<PedidoProducto>();
        List<BusinessLogic.Cliente> clientes = new List<BusinessLogic.Cliente>();
        List<int> mesas = new List<int>();
        
        

        public PedidoMesero()
        {
            InitializeComponent();
            llenarMesas();
            llenarComboBoxMesas();
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

        private void btn_NuevoPedido_Click(object sender, RoutedEventArgs e)
        {
            if (valdiarCampos())
            {

                DataAccess.Pedido pedido = new DataAccess.Pedido();
                pedido.FechaPedido = DateTime.UtcNow;
                pedido.Estatus = 2;
                pedido.NumeroMesa = (int)cbb_mesas.SelectedItem;

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
                if (pedidoController.crearPedidoMesero(pedido, listaproductos) == ResultadoOperacionEnum.ResultadoOperacion.Exito)
                {
                    MessageBox.Show("El Pedido se registró correctamente");
                    listaproductos.Clear();
                    ActualizarDataGrid();
                    lbNuevoPrecio.Text = "";
                }
                else
                {
                    MessageBox.Show("No se puede registar el Pedido");
                }
            }

        }

        private void SalirButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void llenarComboBoxMesas()
        {
            cbb_mesas.ItemsSource = null;
            cbb_mesas.ItemsSource = mesas;
        }

        private void llenarMesas()
        {
            mesas.Add(1);
            mesas.Add(2);
            mesas.Add(3);
            mesas.Add(4);
            mesas.Add(5);
            mesas.Add(6);

        }

        private bool valdiarCampos()
        {
            bool resultado = true;
            if(cbb_mesas.SelectedItem == null)
            {
                resultado = false;
                MessageBox.Show("selecciona una mesa");
            }

            return resultado;
        }

        private void btn_NuevaMesa_Click(object sender, RoutedEventArgs e)
        {
            int last = mesas.Count();
            mesas.Add(last + 1);
            llenarComboBoxMesas();
        }

        private void btn_Cancelar_Click(object sender, RoutedEventArgs e)
        {
            listaproductos.Clear();
            ActualizarDataGrid();
            lbNuevoPrecio.Text = "";
        }
    }
}
