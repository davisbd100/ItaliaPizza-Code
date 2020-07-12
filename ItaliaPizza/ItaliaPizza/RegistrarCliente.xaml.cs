using Controller;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static BusinessLogic.ResultadoOperacionEnum;

namespace ItaliaPizza
{
    /// <summary>
    /// Lógica de interacción para RegistrarCliente.xaml
    /// </summary>
    public partial class RegistrarCliente : Window
    {
        public RegistrarCliente()
        {
            InitializeComponent();
        }

        public string idCliente;

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

        private CheckResult ValidarCamposLlenos()
        {
            CheckResult check = CheckResult.Failed;

            if(textBoxNombre.Text == String.Empty)
            {
                textBoxNombre.BorderBrush = Brushes.Red;
                check = CheckResult.Failed;
            }if(textBoxApellido.Text == String.Empty)
            {
                textBoxApellido.BorderBrush = Brushes.Red;
                check = CheckResult.Failed;
            } else if(textBoxTelefono.Text == String.Empty)
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

        private string GenerarIdCliente()
        {
            Random rdn = new Random();
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890%$#@";
            int longitud = caracteres.Length;
            char letra;
            int longitudId = 10;
            string idAleatorio = string.Empty;
            for (int i = 0; i < longitudId; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                idAleatorio += letra.ToString();
            }
            idCliente = textBoxNombre.Text.ToUpper() + idAleatorio;

            return idCliente;
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

        private void RegistrarButton_Click(object sender, RoutedEventArgs e)
        {
            cambiarColorCampos();
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
                string idPersona = GenerarIdCliente();

                ClienteController clienteController = new ClienteController();

                ComprobarResultado((ResultadoOperacion)clienteController.AgregarCliente(
                    idPersona, nombre, apellido, telefono, correo, ciudad, calle, numero,
                    colonia, codigoPostal, idPersona));
            }
        }

        private  void cambiarColorCampos()
        {
            textBoxNombre.BorderBrush = Brushes.Gray;
            textBoxApellido.BorderBrush = Brushes.Gray;
            textBoxTelefono.BorderBrush = Brushes.Gray;
            textBoxCorreo.BorderBrush = Brushes.Gray;
            textBoxCiudad.BorderBrush = Brushes.Gray;
            textBoxCalle.BorderBrush = Brushes.Gray;
            textBoxNúmero.BorderBrush = Brushes.Gray;
            textBoxColonia.BorderBrush = Brushes.Gray;
            textBoxCodigoPostal.BorderBrush = Brushes.Gray;
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
                    RegistrarCliente registrarCliente = new RegistrarCliente();
                    registrarCliente.Close();
                    break;
            }
        }
    }
}
