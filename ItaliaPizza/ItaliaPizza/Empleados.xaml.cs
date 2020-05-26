using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Controller;

namespace ItaliaPizza
{
    /// <summary>
    /// Lógica de interacción para Empleados.xaml
    /// </summary>
    public partial class Empleados : Window
    {
        public Empleados()
        {
            InitializeComponent();
            //UpdateGrid();
        }

        /// <summary>Actualiza el grid si contiene datos.</summary>
        private void UpdateGrid()
        {
            //EmpleadoController empleadoController = new EmpleadoController();
            //DataGridEmpleados.ItemsSource = null;
            //if (empleadoController.GetEmpleado().Any())
            //{
            //    DataGridEmpleados.ItemsSource = empleadoController.GetEmpleado();
            //}
            //else
            //{
            //    this.Close();
            //}
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrarEmpleado registrarEmpleado = new RegistrarEmpleado();
            registrarEmpleado.ShowDialog();
        }

        private void EditarButton_Click(object sender, RoutedEventArgs e)
        {
            EditarEmpleado editarEmpleado = new EditarEmpleado();
            editarEmpleado.ShowDialog();
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            DarDeBajaEmpleado darDeBajaEmpleado = new DarDeBajaEmpleado();
            darDeBajaEmpleado.ShowDialog();
        }
    }
}
