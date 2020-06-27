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
            if (textBoxNombre.Text == String.Empty || textBoxApellido.Text == String.Empty || 
                textBoxTelefono.Text == String.Empty || textBoxCorreo.Text == String.Empty || 
                textBoxCiudad.Text == String.Empty || textBoxCalle.Text == String.Empty || 
                textBoxNúmero.Text == String.Empty || textBoxColonia.Text == String.Empty || 
                textBoxCodigoPostal.Text == String.Empty)
            {
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
                MessageBox.Show("Existen campos sin llenar");
                check = CheckResult.Failed;
            }
            else if (validarCampos.ValidiarNombre(textBoxNombre.Text) == ItaliaPizza.ValidarCampos.ResultadosValidación.NombreInválido)
            {
                MessageBox.Show("El nombre del cliente es incorrecto \n Verifica que no tenga números o caracteres inválidos.");
            }
            else if (validarCampos.ValidiarApellido(textBoxApellido.Text) == ItaliaPizza.ValidarCampos.ResultadosValidación.ApellidoInválido)
            {
                MessageBox.Show("El apellido del cliente es incorrecto \n Verifica que no tenga números o caracteres inválidos.");
            }
            else if (validarCampos.ValidarTelefono(textBoxTelefono.Text) == ItaliaPizza.ValidarCampos.ResultadosValidación.TelefónoInválido)
            {
                MessageBox.Show("El telefono es incorrecto \n Verifica que no tenga letras.");
            }
            else if (validarCampos.ValidarCorreo(textBoxCorreo.Text) == ItaliaPizza.ValidarCampos.ResultadosValidación.CorreoInválido)
            {
                MessageBox.Show("El correo ingresado no es válido \n Verifica que cuente con el formato correcto.");
            }
            else if (validarCampos.ValidarCodigoPostal(textBoxCodigoPostal.Text) == ItaliaPizza.ValidarCampos.ResultadosValidación.CódigoPostalInválido)
            {
                MessageBox.Show("El código postal ingresado no es válido \n Verifica que solo tenga 5 números.");
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
