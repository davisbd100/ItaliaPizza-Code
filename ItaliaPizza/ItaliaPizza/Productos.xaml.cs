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
            List<Producto> productos = new List<Producto>();

            List<ProductoVenta> listaProductosVenta = RecuperarProductoVenta();
            productos.AddRange(listaProductosVenta);
            dtg_Productos.ItemsSource = productos;
        }

        private List<ProductoIngrediente> RecuperarProductoIngrediente()
        {
            throw new NotImplementedException();
        }

        private List<ProductoVenta> RecuperarProductoVenta()
        {
            ProductoVentaController productoVentaController = new ProductoVentaController();
            List < ProductoVenta > productos = productoVentaController.ObtenerProductoVenta(0);
            return productos;
        }
    }
}
