using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using BusinessLogic;
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
            LlenarGrid();
        }

        /// <summary>Actualiza el grid si contiene datos.</summary>
        private void LlenarGrid()
        {
            DataGridEmpleados.ItemsSource = null;
            EmpleadoController empleadoController = new EmpleadoController();
            List<Empleado> empleados = empleadoController.GetEmpleado(Convert.ToInt32(TextBlockPagina.Text.ToString()));
            DataGridEmpleados.ItemsSource = empleados;
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

        private void ButtonSiguierntePagina_Click(object sender, RoutedEventArgs e)
        {
            int pagina = int.Parse(TextBlockPagina.Text);
            pagina++;
            TextBlockPagina.Text = pagina.ToString();
            LlenarGrid();
        }

        private void ButtonAnteriorPagina_Click(object sender, RoutedEventArgs e)
        {
            int pagina = int.Parse(TextBlockPagina.Text);
            pagina--;
            TextBlockPagina.Text = pagina.ToString();
            LlenarGrid();
        }

        private void ButtonBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxBuscar.Text == "Nombre")
            {
                EmpleadoController empleadoController = new EmpleadoController();
                List<Empleado> empleados = empleadoController.BuscarEmpleado(TextBoxBuscar.Text);
                DataGridEmpleados.ItemsSource = null;
                DataGridEmpleados.ItemsSource = empleados;
            } else if (ComboBoxBuscar.Text == "Calle")
            {
                EmpleadoController empleadoController = new EmpleadoController();
                List<Empleado> empleados = empleadoController.BuscarEmpleadoDireccion(TextBoxBuscar.Text);
                DataGridEmpleados.ItemsSource = null;
                DataGridEmpleados.ItemsSource = empleados;
            }
            else if (ComboBoxBuscar.Text == "Télefono")
            {
                EmpleadoController empleadoController = new EmpleadoController();
                List<Empleado> empleados = empleadoController.BuscarEmpleadoTelefono(TextBoxBuscar.Text);
                DataGridEmpleados.ItemsSource = null;
                DataGridEmpleados.ItemsSource = empleados;
            }
            else
            {
                MessageBox.Show("Seleccione una opcion del filtro");
            }
        }
    }
}
