using Controller;
using DataAccess;
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

namespace ItaliaPizza
{
    /// <summary>
    /// Lógica de interacción para Empleados.xaml
    /// </summary>
    public partial class ReporteDeVentas : Window
    {

        const int POSICION_FUERA_RANGO = -1;
        List<DataAccess.DiaVenta> diaVentas = new List<DiaVenta>();
        InventarioController controller = new InventarioController();
        int PaginaActual = 1;
        int PaginaTotal = 1;

        public ReporteDeVentas()
        {
            InitializeComponent();
            diaVentas = controller.ObtenerDiaVentaPorRango(PaginaActual);
            if (!diaVentas.Any())
            {
                MessageBox.Show("Nunca se ha cerrado un dia");
                this.Close();
            }
            else
            {
                tbPaginaActual.Text = PaginaActual.ToString();
                PaginaTotal = controller.ObtenerPaginasDeTablaDiaVenta();
                tbPaginaTotal.Text = PaginaTotal.ToString();
                dgDiaVenta.ItemsSource = diaVentas;
            }
        }

        private void dgDiaVenta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int posicion = dgDiaVenta.SelectedIndex;

            if (posicion != POSICION_FUERA_RANGO)
            {
                dgDiaProducto.ItemsSource = null;
                dgDiaProducto.ItemsSource = controller.ObtenerProductoVentaDia(((DataAccess.DiaVenta)dgDiaVenta.SelectedValue).idVentaDiaria);
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
                diaVentas = controller.ObtenerDiaVentaPorRango(PaginaActual);
                tbPaginaActual.Text = PaginaActual.ToString();
                dgDiaVenta.ItemsSource = null;
                dgDiaVenta.ItemsSource = diaVentas;

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
                diaVentas = controller.ObtenerDiaVentaPorRango(PaginaActual);
                tbPaginaActual.Text = PaginaActual.ToString();
                dgDiaVenta.ItemsSource = null;
                dgDiaVenta.ItemsSource = diaVentas;

            }
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Funcionalidad no implementada");
        }

        private void SalirButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
