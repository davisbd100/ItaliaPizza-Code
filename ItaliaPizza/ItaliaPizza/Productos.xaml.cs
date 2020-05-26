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
            List<Producto> productos = new List<Producto>();
            List<ProductoVenta> listaProductosVenta = RecuperarProductoVenta();
            List<ProductoIngrediente> listaProductosIngrediente = RecuperarProductoIngrediente();
            productos.AddRange(listaProductosIngrediente);
            productos.AddRange(listaProductosVenta);
            dtg_Productos.ItemsSource = productos;
        }

        private void LlenarGridProductoVenta()
        {
            dtg_Productos.ItemsSource = null;
            List<Producto> productos = new List<Producto>();
            List<ProductoVenta> listaProductosVenta = RecuperarProductoVenta();
            productos.AddRange(listaProductosVenta);
            dtg_Productos.ItemsSource = productos;
        }

        private void LlenarGridIngrediente()
        {
            dtg_Productos.ItemsSource = null;
            List<Producto> productos = new List<Producto>();   
            List<ProductoIngrediente> listaProductosIngrediente = RecuperarProductoIngrediente();
            productos.AddRange(listaProductosIngrediente);
            dtg_Productos.ItemsSource = productos;
        }

        private List<ProductoIngrediente> RecuperarProductoIngrediente()
        {
            ProductoIngredienteController productoIngredienteController = new ProductoIngredienteController();
            List<ProductoIngrediente> productos = productoIngredienteController.ObtenerProductoIngrediente(0);
            return productos;
        }

        private List<ProductoVenta> RecuperarProductoVenta()
        {
            ProductoVentaController productoVentaController = new ProductoVentaController();
            List < ProductoVenta > productos = productoVentaController.ObtenerProductoVenta(0);
            return productos;
        }

        private void btn_actualizarBusqueda_Click(object sender, RoutedEventArgs e)
        {
            if(chkb_Ingredientes.IsChecked == true && chkb_ProductosVenta.IsChecked == true)
            {
                LlenarGrid();
            }
            else if(chkb_Ingredientes.IsChecked == true && chkb_ProductosVenta.IsChecked == false)
            {
                LlenarGridIngrediente();
            }
            else if (chkb_Ingredientes.IsChecked == false && chkb_ProductosVenta.IsChecked == true)
            {
                LlenarGridProductoVenta();
            }


        }
    }
}
