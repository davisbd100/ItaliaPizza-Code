using BusinessLogic;
using Controller;
using System;
using System.Windows;
using System.Windows.Media;
using static BusinessLogic.ResultadoOperacionEnum;

namespace ItaliaPizza
{
    /// <summary>
    /// Lógica de interacción para EditarCliente.xaml
    /// </summary>
    public partial class EditarCliente : Window
    {
        public string idCliente;

        public EditarCliente(Cliente cliente)
        {
            InitializeComponent();
            clienteEditar = cliente;
            CargarCampos();
        }

        Cliente clienteEditar = new Cliente();

        private enum CheckResult
        {
            Passed,
            Failed
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

        private void CargarCampos()
        {
            ClienteController clienteController = new ClienteController();
            Cliente datosCliente = clienteController.GetClienteId(clienteEditar.idPersona);
            textBoxNombre.Text = datosCliente.Nombre;
            textBoxApellido.Text = datosCliente.Apellido;
            textBoxTelefono.Text = datosCliente.Telefono;
            textBoxCorreo.Text = datosCliente.Email;
            textBoxCiudad.Text = datosCliente.Ciudad;
            textBoxCalle.Text = datosCliente.Calle;
            textBoxNúmero.Text = datosCliente.Numero;
            textBoxColonia.Text = datosCliente.Colonia;
            textBoxCodigoPostal.Text = datosCliente.CodigoPostal;
            idCliente = datosCliente.idPersona;
        }

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Close();
                    break;
                case MessageBoxResult.No:
                    EditarCliente editarCliente = new EditarCliente(clienteEditar);
                    editarCliente.Close();
                    break;
            }
        }

        private CheckResult ValidarCamposLlenos()
        {
            CheckResult check = CheckResult.Failed;

            if (textBoxNombre.Text == String.Empty)
            {
                textBoxNombre.BorderBrush = Brushes.Red;
                check = CheckResult.Failed;
            }
            if (textBoxApellido.Text == String.Empty)
            {
                textBoxApellido.BorderBrush = Brushes.Red;
                check = CheckResult.Failed;
            }
            else if (textBoxTelefono.Text == String.Empty)
            {
                textBoxTelefono.BorderBrush = Brushes.Red;
                check = CheckResult.Failed;
            }
            else if (textBoxCorreo.Text == String.Empty)
            {
                textBoxCorreo.BorderBrush = Brushes.Red;
                check = CheckResult.Failed;
            }
            else if (textBoxCiudad.Text == String.Empty)
            {
                textBoxCiudad.BorderBrush = Brushes.Red;
                check = CheckResult.Failed;
            }
            else if (textBoxCalle.Text == String.Empty)
            {
                textBoxCalle.BorderBrush = Brushes.Red;
                check = CheckResult.Failed;
            }
            else if (textBoxNúmero.Text == String.Empty)
            {
                textBoxNúmero.BorderBrush = Brushes.Red;
                check = CheckResult.Failed;
            }
            else if (textBoxColonia.Text == String.Empty)
            {
                textBoxColonia.BorderBrush = Brushes.Red;
                check = CheckResult.Failed;
            }
            else if (textBoxCodigoPostal.Text == String.Empty)
            {
                textBoxCodigoPostal.BorderBrush = Brushes.Red;
                check = CheckResult.Failed;
            }
            else
            {
                check = CheckResult.Passed;
            }
            return check;
        }


        private CheckResult ValidarCampos()
        {
            CheckResult check = CheckResult.Failed;
            ValidarCampos validarCampos = new ValidarCampos();
            if (ValidarCamposLlenos() == CheckResult.Failed)
            {
                MessageBox.Show("Campos vacíos. \n Verifique que todos los capos se encuentren llenos e intente nuevamente.", "Campos vacíos");
                check = CheckResult.Failed;
            }
            else if (validarCampos.ValidiarNombre(textBoxNombre.Text) == ItaliaPizza.ValidarCampos.ResultadosValidación.NombreInválido)
            {
                MessageBox.Show("El nombre del cliente es incorrecto. \n Verifica que no tenga números o caracteres inválidos.", "Nombre inválido");
                textBoxNombre.BorderBrush = Brushes.Red;
            }
            else if (validarCampos.ValidiarApellido(textBoxApellido.Text) == ItaliaPizza.ValidarCampos.ResultadosValidación.ApellidoInválido)
            {
                MessageBox.Show("El apellido del cliente es incorrecto. \n Verifica que no tenga números o caracteres inválidos.", "Apellido inválido");
                textBoxApellido.BorderBrush = Brushes.Red;
            }
            else if (validarCampos.ValidarTelefono(textBoxTelefono.Text) == ItaliaPizza.ValidarCampos.ResultadosValidación.TelefónoInválido)
            {
                MessageBox.Show("El teléfono es incorrecto. \n Verifica que no tenga letras.", "Teléfono inválido");
                textBoxTelefono.BorderBrush = Brushes.Red;
            }
            else if (validarCampos.ValidarCorreo(textBoxCorreo.Text) == ItaliaPizza.ValidarCampos.ResultadosValidación.CorreoInválido)
            {
                MessageBox.Show("El correo ingresado no es válido. \n Verifica que cuente con el formato correcto.", "Correo inválido");
                textBoxCorreo.BorderBrush = Brushes.Red;
            }
            else if (validarCampos.ValidarCodigoPostal(textBoxCodigoPostal.Text) == ItaliaPizza.ValidarCampos.ResultadosValidación.CódigoPostalInválido)
            {
                MessageBox.Show("El código postal ingresado no es válido. \n Verifica que solo tenga 5 números.", "Código postal inválido");
                textBoxCodigoPostal.BorderBrush = Brushes.Red;
            }
            else
            {
                check = CheckResult.Passed;
            }
            return check;
        }

        private void ComprobarResultado(ResultadoOperacion resultado)
        {
            if (resultado == ResultadoOperacion.Exito)
            {
                MessageBox.Show("Añadido con exito");
                this.Close();
            }
            else if (resultado == ResultadoOperacion.FallaDesconocida)
            {
                MessageBox.Show("Error desconocido");
            }
            else if (resultado == ResultadoOperacion.FalloSQL)
            {
                MessageBox.Show("Error de la base de datos, intente mas tarde");
            }
            else if (resultado == ResultadoOperacion.ObjetoExistente)
            {
                MessageBox.Show("El empleado ya existe en el sistema");
            }
        }

        private void EditarButton_Click(object sender, RoutedEventArgs e)
        {
            string idPersona = idCliente;
            string nombre = textBoxNombre.Text.Trim();
            string apellido = textBoxApellido.Text.Trim();
            string telefono = textBoxTelefono.Text.Trim();
            string correo = textBoxCorreo.Text.Trim();
            string ciudad = textBoxCiudad.Text.Trim();
            string calle = textBoxCalle.Text.Trim();
            string numero = textBoxNúmero.Text.Trim();
            string colonia = textBoxColonia.Text.Trim();
            string codigoPostal = textBoxCodigoPostal.Text.Trim();

            if (ValidarCampos() == CheckResult.Passed)
            {
                ClienteController clienteController = new ClienteController();
                ComprobarResultado((ResultadoOperacion)clienteController.EditarCliente(
                    idPersona, nombre, apellido, telefono, correo, ciudad, calle, numero,
                    colonia, codigoPostal, idCliente));
            }
        }
    }
}
