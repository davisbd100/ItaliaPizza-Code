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
        String BusquedaActual;
        public ProductosUC()
        {
            InitializeComponent();
            productos = new List<ProductoVenta>();
            PaginaTotal = Controller.ObtenerPaginasDeTablaProductoVenta();
        }

        private void btBuscar_Click(object sender, RoutedEventArgs e)
        {
            BusquedaActual = tbBusqueda.Text;
            tbPaginaTotal.Text = PaginaTotal.ToString();
            if (tbBusqueda.Text == "")
            {
                PaginaTotal = Controller.ObtenerPaginasDeTablaProductoVenta();
                productos = Controller.ObtenerProductosVenta(PaginaActual);
            }
            else
            {
                productos = Controller.ObtenerProductoVentaPorRangoBusqueda(PaginaActual, BusquedaActual);
                var tempTotal = productos.Count / 20;
                PaginaTotal = (int)Math.Ceiling((double)tempTotal);
            }
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
                productos = Controller.ObtenerProductoVentaPorRangoBusqueda(PaginaActual, BusquedaActual);
                tbPaginaActual.Text = PaginaActual.ToString();
                icProductos.ItemsSource = null;
                icProductos.ItemsSource = productos;
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
                productos = Controller.ObtenerProductoVentaPorRangoBusqueda(PaginaActual, BusquedaActual);
                tbPaginaActual.Text = PaginaActual.ToString();
                icProductos.ItemsSource = null;
                icProductos.ItemsSource = productos;
            }
        }
    }
}
