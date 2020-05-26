﻿using BusinessLogic;
using Controller;
using System;
using System.Windows;
using static BusinessLogic.ResultadoOperacionEnum;
using static Controller.EmpleadoController;

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
            string nombre = textBoxNombre.Text;
            string apellido = textBoxApellido.Text;
            string telefono = textBoxTelefono.Text;
            string correo = textBoxCorreo.Text;
            string ciudad = textBoxCiudad.Text;
            string calle = textBoxCalle.Text;
            string numero = textBoxNúmero.Text;
            string colonia = textBoxColonia.Text;
            string codigoPostal = textBoxCodigoPostal.Text;
            string usuario = textBoxUsuario.Text;
            string contraseña = textBoxContraseña.Text;
            string tipoEmpleado = comboBoxTipoEmpleado.Text;

            if (ValidarCampos() == CheckResult.Passed)
            {
                EmpleadoController empleadoController = new EmpleadoController();
                ComprobarResultado((ResultadoOperacion)empleadoController.AgregarEmpleado(nombre, apellido, telefono, correo, ciudad, calle, numero, colonia, codigoPostal, usuario, contraseña, tipoEmpleado));
            }
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

        private void GenerarUsuarioContraseñaButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCampos() == CheckResult.Passed)
            {
                GenerarUsuario(comboBoxTipoEmpleado.Text, textBoxNombre.Text);
                GenerarContraseñaAleatoria();

                DateTime fechaAcceso = DateTime.Now;
                textBoxFecha.Text = DateTime.Now.ToString();
                RegistrarButton.IsEnabled = true;
            }
        }
    }
}
