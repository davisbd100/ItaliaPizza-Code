using BusinessLogic;
using Controller;
using System.Windows;
using static BusinessLogic.ResultadoOperacionEnum;

namespace ItaliaPizza
{
    /// <summary>
    /// Lógica de interacción para DarDeBajaCliente.xaml
    /// </summary>
    public partial class DarDeBajaCliente : Window
    {
        public DarDeBajaCliente(Cliente clienteEliminar)
        {
            InitializeComponent();
            clienteC = clienteEliminar;
        }

        public enum OperationResult
        {
            Success,
            NullOrganization,
            InvalidOrganization,
            UnknowFail,
            SQLFail,
            ExistingRecord
        }

        Cliente clienteC = new Cliente();

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Close();
                    break;
                case MessageBoxResult.No:
                    RegistrarCliente registrarCliente = new RegistrarCliente();
                    registrarCliente.Close();
                    break;
            }
        }

        private void AceptarButton_Click(object sender, RoutedEventArgs e)
        {
            EliminarCliente();
        }

        private void EliminarCliente()
        {
            ClienteController clienteController = new ClienteController();
            if(clienteController.EliminarCliente(clienteC) == ResultadoOperacion.Exito)
            {
                MessageBox.Show("Cliente eliminado con éxito", "Éxito");
                this.Close();
            } else
            {
                MessageBox.Show("No se pudo eliminar el cliente", "Error");
                this.Close();
            }
        }
    }
}
