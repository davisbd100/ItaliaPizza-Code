using BusinessLogic;
using Controller;
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
using System.Windows.Shapes;

namespace ItaliaPizza
{
    /// <summary>
    /// Lógica de interacción para Clientes.xaml
    /// </summary>
    public partial class Clientes : Window
    {
        private int POSICION_FUERA_RANGO = -1;

        public Clientes()
        {
            InitializeComponent();
            LlenarGrid();
        }

        private void LlenarGrid()
        {
            DataGridClientes.ItemsSource = null;
            ClienteController clienteController = new ClienteController();
            List<Cliente> clientes = clienteController.GetCliente(Convert.ToInt32(TextBlockPagina.Text.ToString()));
            DataGridClientes.ItemsSource = clientes;
        }

        private void ButtonAnteriorPagina_Click(object sender, RoutedEventArgs e)
        {
            int pagina = int.Parse(TextBlockPagina.Text);
            pagina--;
            TextBlockPagina.Text = pagina.ToString();
            LlenarGrid();
        }

        private void ButtonSiguierntePagina_Click(object sender, RoutedEventArgs e)
        {
            int pagina = int.Parse(TextBlockPagina.Text);
            pagina++;
            TextBlockPagina.Text = pagina.ToString();
            LlenarGrid();
        }

        private void SalirButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrarCliente registrarCliente = new RegistrarCliente();
            registrarCliente.ShowDialog();
            LlenarGrid();
        }

        private void EditarButton_Click(object sender, RoutedEventArgs e)
        {
            int posicion = DataGridClientes.SelectedIndex;

            if (posicion != POSICION_FUERA_RANGO && ValidarSeleccion())
            {
                Cliente cliente = (Cliente)DataGridClientes.SelectedItem;
                EditarCliente editarCliente = new EditarCliente(cliente);
                editarCliente.ShowDialog();
                LlenarGrid();
            }
            else
            {
                MessageBox.Show("Debes seleccionar solo un empleado", "Error");
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            int posicion = DataGridClientes.SelectedIndex;

            if (posicion != POSICION_FUERA_RANGO && ValidarSeleccion())
            {
                Cliente cliente = (Cliente)DataGridClientes.SelectedItem;

                DarDeBajaCliente darDeBajaCliente = new DarDeBajaCliente(cliente);
                darDeBajaCliente.ShowDialog();
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
            if (DataGridClientes.SelectedItems.Count == 1)
            {
                resultado = true;
            }
            return resultado;
        }

        private void ButtonBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxBuscar.Text == "Nombre")
            {
                ClienteController clienteController = new ClienteController();
                List<Cliente> clientes = clienteController.BuscarCliente(TextBoxBuscar.Text);
                DataGridClientes.ItemsSource = null;
                DataGridClientes.ItemsSource = clientes;
            }
            else if (ComboBoxBuscar.Text == "Calle")
            {
                ClienteController clienteController = new ClienteController();
                List<Cliente> clientes = clienteController.BuscarClienteDireccion(TextBoxBuscar.Text);
                DataGridClientes.ItemsSource = null;
                DataGridClientes.ItemsSource = clientes;
            }
            else if (ComboBoxBuscar.Text == "Télefono")
            {
                ClienteController clienteController = new ClienteController();
                List<Cliente> clientes = clienteController.BuscarClienteTelefono(TextBoxBuscar.Text);
                DataGridClientes.ItemsSource = null;
                DataGridClientes.ItemsSource = clientes;
            }
            else
            {
                ClienteController clienteController = new ClienteController();
                List<Cliente> clientes = clienteController.BuscarCliente(TextBoxBuscar.Text);
                DataGridClientes.ItemsSource = null;
                DataGridClientes.ItemsSource = clientes;
            }
        }
    }
}
