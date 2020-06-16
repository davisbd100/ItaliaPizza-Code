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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Controller;
using BusinessLogic;

namespace ItaliaPizza.Controls
{
    /// <summary>
    /// Interaction logic for ProductosUC.xaml
    /// </summary>
    public partial class ProductosUC : UserControl
    {
        ProductoVentaController Controller = new ProductoVentaController();
        List<ProductoVenta> productos;
        public ProductosUC()
        {
            InitializeComponent();
            productos = new List<ProductoVenta>();
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            productos = Controller.ObtenerProductosVenta(1);
            icProductos.ItemsSource = null;
            icProductos.ItemsSource = productos;
        }
    }
}
