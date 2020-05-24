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
    /// Lógica de interacción para RegistrarEmpleado.xaml
    /// </summary>
    public partial class RegistrarEmpleado : Window
    {
        public RegistrarEmpleado()
        {
            InitializeComponent();
        }

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

        /// <summary>Verifica si los campos ingresados estan vacios.</summary>
        /// <returns>El resultado de la verificación</returns>
        private CheckResult ValidarCamposLlenos()
        {
            CheckResult check = CheckResult.Failed;
            if (comboBoxTipoEmpleado.Text == String.Empty || textBoxNombre.Text == String.Empty || textBoxTelefono.Text == String.Empty || textBoxCorreo.Text == String.Empty || textBoxCiudad.Text == String.Empty ||
                textBoxDireccion.Text == String.Empty || textBoxCodigoPostal.Text == String.Empty)
            {
                check = CheckResult.Failed;
            }
            else
            {
                check = CheckResult.Passed;
            }
            return check;
        }

        /// <summary>Checa por datos no validos en los datos ingresados.</summary>
        /// <returns>El resultado del chequeo</returns>
        private CheckResult ValidarCampos()
        {
            CheckResult check = CheckResult.Failed;
            ValidarCampos validarCampos = new ValidarCampos();
            if (ValidarCamposLlenos() == CheckResult.Failed)
            {
                MessageBox.Show("Existen campos sin llenar");
                check = CheckResult.Failed;
            }
            else if (comboBoxTipoEmpleado.Text == null)
            {
                MessageBox.Show("Debes seleccionar un tipo de empleado de la lista.");
            }
            else if (validarCampos.ValidiarNombre(textBoxNombre.Text) == ItaliaPizza.ValidarCampos.ResultadosValidación.NombreInválido)
            {
                MessageBox.Show("El nombre del empleado es incorrecto \n Verifica que no tenga números o caracteres inválidos.");
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

        /// <summary>Comprueba el resultado de la operacion.</summary>
        /// <param name="result">El resultado.</param>
        private void ComprobarResultado(OperationResult result)
        {
            if (result == OperationResult.Success)
            {
                MessageBox.Show("Añadido con exito");
                this.Close();
            }
            else if (result == OperationResult.UnknowFail)
            {
                MessageBox.Show("Error desconocido");
            }
            else if (result == OperationResult.SQLFail)
            {
                MessageBox.Show("Error de la base de datos, intente mas tarde");
            }
            else if (result == OperationResult.ExistingRecord)
            {
                MessageBox.Show("El empleado ya existe en el sistema");
            }
        }

        private void GenerarUsuario(string tipoEmpleado, string nombre)
        {
            Random random = new Random();
            tipoEmpleado = comboBoxTipoEmpleado.Text.Trim();
            nombre = textBoxNombre.Text.Trim();
            int longitudNombreUsuario = 4;
            char nombreUsuario;
            int longitudNombre = nombre.Length;
            string usuarioAleatorio = string.Empty;

            for(int i = 0; i < longitudNombreUsuario; i++)
            {
                nombreUsuario = nombre[random.Next(longitudNombre)];
                usuarioAleatorio += nombreUsuario.ToString();
            }

            textBoxUsuario.Text = tipoEmpleado + usuarioAleatorio.Trim();
        }

        private void GenerarContraseñaAleatoria()
        {
            Random rdn = new Random();
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890%$#@";
            int longitud = caracteres.Length;
            char letra;
            int longitudContrasenia = 10;
            string contraseniaAleatoria = string.Empty;
            for (int i = 0; i < longitudContrasenia; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                contraseniaAleatoria += letra.ToString();
            }

            textBoxContraseña.Text = contraseniaAleatoria;
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
                    RegistrarEmpleado registrarEmpleado = new RegistrarEmpleado();
                    registrarEmpleado.Close();
                    break;
            }
        }

        private void RegistrarButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos() == CheckResult.Passed)
            {

                MessageBox.Show("Hola");
                //        UsuarioController usuarioController = new UsuarioController();
                //        ComprobarResultado((OperationResult)usuarioController.AddUsuario(textboxName.Text, textboxEmail.Text, comboboxUserType.Text, textboxUserName.Text, passwordBoxUserPass.Password));
            }
        }

    private void GenerarUsuarioContraseñaButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos() == CheckResult.Passed)
            {
                GenerarUsuario(comboBoxTipoEmpleado.Text, textBoxNombre.Text);
                GenerarContraseñaAleatoria();
                RegistrarButton.IsEnabled = true;
            }
        }
    }
}
