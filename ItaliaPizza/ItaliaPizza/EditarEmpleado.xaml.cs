using BusinessLogic;
using Controller;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using static BusinessLogic.ResultadoOperacionEnum;

namespace ItaliaPizza
{
    /// <summary>
    /// Lógica de interacción para EditarEmpleado.xaml
    /// </summary>
    public partial class EditarEmpleado : Window
    {
        public EditarEmpleado(Empleado empleado)
        {
            InitializeComponent();
            empleadoEditar = empleado;
            CargarCampos();
        }

        Empleado empleadoEditar = new Empleado();

        private void CargarCampos()
        {
            EmpleadoController empleadoController = new EmpleadoController();
            Empleado datosEmpleado = empleadoController.GetEmpleadoId(empleadoEditar.idPersona);
            comboBoxTipoEmpleado.Text = datosEmpleado.TipoEmpleado;
            textBoxNombre.Text = datosEmpleado.Nombre;
            textBoxApellido.Text = datosEmpleado.Apellido;
            textBoxTelefono.Text = datosEmpleado.Telefono;
            textBoxCorreo.Text = datosEmpleado.Email;
            textBoxCiudad.Text = datosEmpleado.Ciudad;
            textBoxCalle.Text = datosEmpleado.Calle;
            textBoxNúmero.Text = datosEmpleado.Numero;
            textBoxColonia.Text = datosEmpleado.Colonia;
            textBoxCodigoPostal.Text = datosEmpleado.CodigoPostal;
            textBoxIdEmpleado.Text = datosEmpleado.idEmpleado;
            textBoxUsuario.Text = datosEmpleado.NombreUsuario;
            //passwordBoxContraseña.Password = datosEmpleado.Contraseña;
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
            if (comboBoxTipoEmpleado.Text == String.Empty || textBoxNombre.Text == String.Empty ||
                textBoxApellido.Text == String.Empty || textBoxTelefono.Text == String.Empty ||
                textBoxCorreo.Text == String.Empty || textBoxCiudad.Text == String.Empty ||
                textBoxCalle.Text == String.Empty || textBoxNúmero.Text == String.Empty ||
                textBoxColonia.Text == String.Empty || textBoxCodigoPostal.Text == String.Empty)
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
            else if (comboBoxTipoEmpleado.Text == null)
            {
                MessageBox.Show("Debes seleccionar un tipo de empleado de la lista.");
            }
            else if (validarCampos.ValidiarNombre(textBoxNombre.Text) == ItaliaPizza.ValidarCampos.ResultadosValidación.NombreInválido)
            {
                MessageBox.Show("El nombre del empleado es incorrecto \n Verifica que no tenga números o caracteres inválidos.");
            }
            else if (validarCampos.ValidiarApellido(textBoxApellido.Text) == ItaliaPizza.ValidarCampos.ResultadosValidación.ApellidoInválido)
            {
                MessageBox.Show("El apellido del empleado es incorrecto \n Verifica que no tenga números o caracteres inválidos.");
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

        private void AceptarButton_Click(object sender, RoutedEventArgs e)
        {
            //string idPersona = textBoxIdEmpleado.Text.Trim();
            //string nombre = textBoxNombre.Text.Trim();
            //string apellido = textBoxApellido.Text.Trim();
            //string telefono = textBoxTelefono.Text.Trim();
            //string correo = textBoxCorreo.Text.Trim();
            //string ciudad = textBoxCiudad.Text.Trim();
            //string calle = textBoxCalle.Text.Trim();
            //string numero = textBoxNúmero.Text.Trim();
            //string colonia = textBoxColonia.Text.Trim();
            //string codigoPostal = textBoxCodigoPostal.Text.Trim();
            //string idEmpleado = textBoxIdEmpleado.Text.Trim();
            //string usuario = textBoxUsuario.Text.Trim();
            //string contraseña = passwordBoxContraseña.Password;
            //string tipoEmpleado = comboBoxTipoEmpleado.Text.Trim();

            //if (ValidarCampos() == CheckResult.Passed)
            //{
            //    EmpleadoController empleadoController = new EmpleadoController();
            //    ComprobarResultado((ResultadoOperacion)empleadoController.EditarEmpleado(
            //        idPersona, nombre, apellido, telefono, correo, ciudad, calle, numero,
            //        colonia, codigoPostal));
            //}
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
    }
}
