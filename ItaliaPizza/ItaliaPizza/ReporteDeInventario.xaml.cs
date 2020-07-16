using Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ItaliaPizza
{
    /// <summary>
    /// Lógica de interacción para Empleados.xaml
    /// </summary>
    public partial class ReporteDeInventario : Window
    {
        private class CustomInventario: DataAccess.Inventario
        {
            public String NombreProducto { get; set; }
            public String CodigoProducto { get; set; }
        }

        const int POSICION_FUERA_RANGO = -1;
        List<CustomInventario> inventario = new List<CustomInventario>();
        InventarioController controller = new InventarioController();
        int PaginaActual = 1;
        int PaginaTotal = 1;

        public ReporteDeInventario()
        {
            InitializeComponent();
            foreach (var item in controller.ObtenerIngresosInventario(PaginaActual))
            {
                CustomInventario customInventario = new CustomInventario()
                {
                    idInventario = item.idInventario,
                    ExistenciaInicial = item.ExistenciaInicial,
                    ExistenciaTotal = item.ExistenciaTotal,
                    UnidadMedida = item.UnidadMedida,
                    Producto = item.Producto
                };
                ProductoController productoController = new ProductoController();
                DataAccess.Producto tempProducto = productoController.ObtenerProductoPorId((int)customInventario.Producto);
                customInventario.NombreProducto = tempProducto.Nombre;
                customInventario.CodigoProducto = tempProducto.Codigo;
                inventario.Add(customInventario);
            } 
            if (!inventario.Any())
            {
                MessageBox.Show("No se tienen productos registrados");
                this.Close();
            }
            else
            {
                tbPaginaActual.Text = PaginaActual.ToString();
                PaginaTotal = controller.ObtenerPaginasDeTablaInventario();
                tbPaginaTotal.Text = PaginaTotal.ToString();
                dgInventario.ItemsSource = inventario;
            }
        }


        private void btBusqueda_Click(object sender, RoutedEventArgs e)
        {
            string filter = tbBusqueda.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(dgInventario.ItemsSource);
            if (filter == "")
                cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    CustomInventario p = o as CustomInventario;
                    if (tbBusqueda.Name == "tbBusqueda")
                        return (p.NombreProducto == filter);
                    return (p.NombreProducto.ToUpper().StartsWith(filter.ToUpper()));
                };
            }
        }

        private void dgInventario_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int posicion = dgInventario.SelectedIndex;

            if (posicion != POSICION_FUERA_RANGO)
            {
                dgPedidoProducto.ItemsSource = null;
                dgPedidoProducto.ItemsSource = controller.ObtenerProductoInventario(((CustomInventario)dgInventario.SelectedValue).idInventario);
            }
            else
            {
                MessageBox.Show("Debes seleccionar solo un producto");
            }
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
                inventario = controller.ObtenerIngresosInventario(PaginaActual).ConvertAll(b => (CustomInventario)b);
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
                inventario = controller.ObtenerIngresosInventario(PaginaActual).ConvertAll(b => (CustomInventario)b);
                tbPaginaActual.Text = PaginaActual.ToString();
                dgInventario.ItemsSource = null;
                dgInventario.ItemsSource = inventario;

            }
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            if (true)
            {
                MessageBox.Show("Funcion no implementada");
            }
            else
            {
                ContenedorReporteInventario reporteInventario = new ContenedorReporteInventario();
            }
        }

        private void SalirButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btIngresarManualmente_Click(object sender, RoutedEventArgs e)
        {
            InventarioActual inventarioActual = new InventarioActual();
            inventarioActual.ShowDialog();
            this.Close();
        }
    }
}
