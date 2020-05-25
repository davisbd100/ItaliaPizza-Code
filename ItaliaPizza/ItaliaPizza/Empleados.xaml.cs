using System.Windows;
using System.Windows.Controls;

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
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
