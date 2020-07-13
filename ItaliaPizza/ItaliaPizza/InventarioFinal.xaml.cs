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

namespace PrototiposItaliaPizza
{
    /// <summary>
    /// Lógica de interacción para Empleados.xaml
    /// </summary>
    public partial class InventarioFinal : Window
    {
        const int POSICION_FUERA_RANGO = -1;
        List<DataAccess.Inventario> inventario = new List<DataAccess.Inventario>();
        InventarioController controller = new InventarioController();
        int PaginaActual = 1;
        int PaginaTotal = 1;

        public InventarioFinal()
        {
            InitializeComponent();
            inventario = controller.ObtenerIngresosInventario(PaginaActual);
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
                    DataAccess.Inventario p = o as DataAccess.Inventario;
                    if (tbBusqueda.Name == "tbBusqueda")
                        return (p.Producto1.Nombre == filter);
                    return (p.Producto1.Nombre.ToUpper().StartsWith(filter.ToUpper()));
                };
            }
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dgInventario_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int posicion = dgInventario.SelectedIndex;

            if (posicion != POSICION_FUERA_RANGO)
            {
                dgPedidoProducto.ItemsSource = null;
                dgPedidoProducto.ItemsSource = ((DataAccess.Inventario)dgInventario.SelectedItem).ProductoInventario;
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
                inventario = controller.ObtenerIngresosInventario(PaginaActual);
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
                inventario = controller.ObtenerIngresosInventario(PaginaActual);
                tbPaginaActual.Text = PaginaActual.ToString();
                dgInventario.ItemsSource = null;
                dgInventario.ItemsSource = inventario;

            }
        }
    }
}
