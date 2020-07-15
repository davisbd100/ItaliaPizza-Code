using System;
using System.Collections.Generic;
using System.Windows;
using BusinessLogic;
using Controller;

namespace ItaliaPizza
{
    /// <summary>
    /// Lógica de interacción para Empleados.xaml
    /// </summary>
    public partial class Empleados : Window
    {
        private int POSICION_FUERA_RANGO = -1;

        public Empleados()
        {
            InitializeComponent();
            LabelNombre.Content = Properties.Settings.Default.NombreEmpleado;
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
            LlenarGrid();
        }

        private void EditarButton_Click(object sender, RoutedEventArgs e)
        {
            int posicion = DataGridEmpleados.SelectedIndex;

            if (posicion != POSICION_FUERA_RANGO && ValidarSeleccion())
            {
                Empleado empleado = (Empleado)DataGridEmpleados.SelectedItem;
                EditarEmpleado editarEmpleado = new EditarEmpleado(empleado);
                editarEmpleado.ShowDialog();
                LlenarGrid();
            }
            else
            {
                MessageBox.Show("Debes seleccionar solo un empleado", "Error");
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int posicion = DataGridEmpleados.SelectedIndex;

            if (posicion != POSICION_FUERA_RANGO && ValidarSeleccion())
            {
                Empleado empleado = (Empleado)DataGridEmpleados.SelectedItem;

                DarDeBajaEmpleado darDeBajaEmpleado = new DarDeBajaEmpleado(empleado);
                darDeBajaEmpleado.ShowDialog();
                LlenarGrid();
            }
            else
            {
                MessageBox.Show("Debes seleccionar solo un empleado", "Error");
            }
        }

        private bool ValidarSeleccion()
        {
            bool resultado = false;
            if (DataGridEmpleados.SelectedItems.Count == 1)
            {
                resultado = true;
            }
            return resultado;
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
            }
            else if (ComboBoxBuscar.Text == "Calle")
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
                EmpleadoController empleadoController = new EmpleadoController();
                List<Empleado> empleados = empleadoController.BuscarEmpleado(TextBoxBuscar.Text);
                DataGridEmpleados.ItemsSource = null;
                DataGridEmpleados.ItemsSource = empleados;
            }
        }

        private void SalirButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
