using BusinessLogic;
using Controller;
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
    /// Interaction logic for Productos.xaml
    /// </summary>
    public partial class Productos : Window
    {
        public Productos()
        {
            InitializeComponent();
            LlenarGrid();
        }

        private void LlenarGrid()
        {
            dtg_Productos.ItemsSource = null;
            ProductoController productoController = new ProductoController();
            List<Producto> productos = productoController.ObtenerProducto( Convert.ToInt32 ( txb_Pagina.Text.ToString()));
            dtg_Productos.ItemsSource = productos;
        }

        private void LlenarGridProductoVenta()
        {
            dtg_Productos.ItemsSource = null;
            ProductoVentaController productoVentaController = new ProductoVentaController();
            List<Producto> productos = new List<Producto>();
            List<ProductoVenta> listaProductosVenta = productoVentaController.ObtenerProductoVenta(Convert.ToInt32(txb_Pagina.Text.ToString()));
            productos.AddRange(listaProductosVenta);
            dtg_Productos.ItemsSource = productos;
        }

        private void LlenarGridIngrediente()
        {
            dtg_Productos.ItemsSource = null;
            ProductoIngredienteController productoIngredienteController = new ProductoIngredienteController();
            List<Producto> productos = new List<Producto>();   
            List<ProductoIngrediente> listaProductosIngrediente = productoIngredienteController.ObtenerProductoIngrediente(Convert.ToInt32(txb_Pagina.Text.ToString()));
            productos.AddRange(listaProductosIngrediente);
            dtg_Productos.ItemsSource = productos;
        }

        private void ActualizarLista()
        {
            if (chkb_Ingredientes.IsChecked == true && chkb_ProductosVenta.IsChecked == true)
            {
                LlenarGrid();
            }
            else if (chkb_Ingredientes.IsChecked == true && chkb_ProductosVenta.IsChecked == false)
            {
                LlenarGridIngrediente();
            }
            else if (chkb_Ingredientes.IsChecked == false && chkb_ProductosVenta.IsChecked == true)
            {
                LlenarGridProductoVenta();
            }
        }


        private void btn_actualizarBusqueda_Click(object sender, RoutedEventArgs e)
        {
            txb_Pagina.Text = "1";
            ActualizarLista();

        }

        private void btn_SigPag_Click(object sender, RoutedEventArgs e)
        {
            int pagina = int.Parse( txb_Pagina.Text);
            pagina++;
            txb_Pagina.Text = pagina.ToString();
            ActualizarLista();

        }

        private void btn_buscar_Click(object sender, RoutedEventArgs e)
        {
            ProductoController productoController = new ProductoController();
            List < Producto > productos = productoController.Buscarproducto(txb_busqueda.Text);
            dtg_Productos.ItemsSource = null;
            dtg_Productos.ItemsSource = productos;

        }
    }
}
