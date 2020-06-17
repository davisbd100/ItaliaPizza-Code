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
        int PaginaActual = 1;
        int PaginaTotal = 1;
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
        private void btPaginaAnterior_Click(object sender, RoutedEventArgs e)
        {
            if ((PaginaActual - 1) < 1)
            {
                MessageBox.Show("No se puede regresar mas");
            }
            else
            {
                PaginaActual--;
                productos = Controller.prod(PaginaActual);
                tbPaginaActual.Text = PaginaActual.ToString();
                dgInventario.ItemsSource = null;
                dgInventario.ItemsSource = inventario;

            }
        }

        private void btPaginaSiguiente_Click(object sender, RoutedEventArgs e)
        {

            if (PaginaActual == PaginaTotal)
            {
                MessageBox.Show("No se puede avanzar mas");
            }
            else
            {
                PaginaActual++;
                inventario = controller.ObtenerInventarioPorRango(PaginaActual);
                tbPaginaActual.Text = PaginaActual.ToString();
                dgInventario.ItemsSource = null;
                dgInventario.ItemsSource = inventario;

            }
        }
    }
}
