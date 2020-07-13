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

namespace ItaliaPizza
{
    /// <summary>
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void ButtonProductos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonRecetas_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonInventario_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonVentas_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonClientes_Click(object sender, RoutedEventArgs e)
        {
            Clientes clientes = new Clientes();
            abrirVentana(clientes);
        }

        private void ButtonEmpleados_Click(object sender, RoutedEventArgs e)
        {
            Empleados empleados = new Empleados();
            abrirVentana(empleados);
        }

        public void abrirVentana(Window window)
        {
            window.Show();
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
        }
    }
}
