using AutenticacionInicioSesion;
using Controller;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static AutenticacionInicioSesion.Autenticacion;

namespace ItaliaPizza
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Inicio : Window
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void IniciarButton_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void Login()
        {
            AutenticacionController autenticacion = new AutenticacionController();
            DatosLogin datosLogin = autenticacion.AutenticacionEmpleado(NombreUsuarioTextBox.Text.ToString(), ContaseñaPasswordBox.Password);
            if (datosLogin.Result.Equals(validationResult.PasswordIncorrect))
            {
                MessageBox.Show("Usuario y/o contraseña incorrecto");
                ContaseñaPasswordBox.Password = String.Empty;
            } else if (datosLogin.Result.Equals(validationResult.Success))
            {
                Properties.Settings.Default.EmpleadoID = autenticacion.GetUserName(NombreUsuarioTextBox.Text.ToString(), ContaseñaPasswordBox.Password);
                Properties.Settings.Default.EmpleadoType = autenticacion.GetUserType(NombreUsuarioTextBox.Text.ToString(), ContaseñaPasswordBox.Password);
                AbrirVentana();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error no identificado");
            }
        }

        private void AbrirVentana()
        {
            string typeEmpleado = Properties.Settings.Default.EmpleadoType;
            switch (typeEmpleado)
            {
                case "Gerente":
                    Empleados empleados = new Empleados();
                    empleados.Show();
                    this.Close();
                    break;
                case "Administrador BD":
                    
                    break;
                case "Cajero":
                    
                    break;
                case "Call Center":

                    break;
                case "Cocinero":

                    break;
                case "Mesero":

                    break;
            }
        }

        private bool validarCampos(string nombreUsuario, string contraseñaUsuario)
        {
            bool camposValidos = false;

            if ((nombreUsuario != "") && (contraseñaUsuario != ""))
            {
                camposValidos = true;
                return camposValidos;
            }
            else
            {
                return camposValidos;
            }
        }

        private void VerContraseñaButton_MouseEnter(object sender, MouseEventArgs e)
        {
            VerContraseñaTextBox.Visibility = Visibility.Visible;
            ContaseñaPasswordBox.Visibility = Visibility.Hidden;
            VerContraseñaTextBox.Text = ContaseñaPasswordBox.Password;
        }

        private void VerContraseñaButton_MouseLeave(object sender, MouseEventArgs e)
        {
            VerContraseñaTextBox.Visibility = Visibility.Hidden;
            ContaseñaPasswordBox.Visibility = Visibility.Visible;
            VerContraseñaTextBox.Text = String.Empty;
        }
    }
}
