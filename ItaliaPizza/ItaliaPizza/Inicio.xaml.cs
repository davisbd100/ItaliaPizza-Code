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
            if(validarCampos(NombreUsuarioTextBox.Text, ContraseñaPasswordBox.Password) == true)
            {
                Login();
            }
            else
            {
                MessageBox.Show("Existen campos vacíos, asegúrate de que todos estén llenos", "Campos vacíos");
            }
            
        }

        private void Login()
        {
            AutenticacionController autenticacion = new AutenticacionController();
            DatosLogin datosLogin = autenticacion.AutenticacionEmpleado(NombreUsuarioTextBox.Text.ToString(), ContraseñaPasswordBox.Password);
            if (datosLogin.Result.Equals(validationResult.PasswordIncorrect))
            {
                MessageBox.Show("Usuario y/o contraseña incorrecta, verifica que sean correctos", "Datos incorrectos");
                ContraseñaPasswordBox.Password = String.Empty;
            } else if (datosLogin.Result.Equals(validationResult.Success))
            {
                Properties.Settings.Default.EmpleadoID = autenticacion.GetUserName(NombreUsuarioTextBox.Text.ToString(), ContraseñaPasswordBox.Password);
                Properties.Settings.Default.EmpleadoType = autenticacion.GetUserType(NombreUsuarioTextBox.Text.ToString(), ContraseñaPasswordBox.Password);
                AbrirVentana();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error no identificado, intente nuevamente", "Error");
            }
        }

        private void AbrirVentana()
        {
            string typeEmpleado = Properties.Settings.Default.EmpleadoType;
            switch (typeEmpleado)
            {
                case "Gerente":
                    Productos productos = new Productos();
                    productos.Show();
                    this.Close();
                    break;
                case "Administrador BD":
                    Empleados empleadosA = new Empleados();
                    empleadosA.Show();
                    this.Close();
                    break;
                case "Cajero":
                    Empleados empleadosC = new Empleados();
                    empleadosC.Show();
                    this.Close();
                    break;
                case "Call Center":
                    Empleados empleadosCC = new Empleados();
                    empleadosCC.Show();
                    this.Close();
                    break;
                case "Cocinero":
                    Empleados empleadosCo = new Empleados();
                    empleadosCo.Show();
                    this.Close();
                    break;
                case "Mesero":
                    Empleados empleadosM = new Empleados();
                    empleadosM.Show();
                    this.Close();
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
            ContraseñaPasswordBox.Visibility = Visibility.Hidden;
            VerContraseñaTextBox.Text = ContraseñaPasswordBox.Password;
        }

        private void VerContraseñaButton_MouseLeave(object sender, MouseEventArgs e)
        {
            VerContraseñaTextBox.Visibility = Visibility.Hidden;
            ContraseñaPasswordBox.Visibility = Visibility.Visible;
            VerContraseñaTextBox.Text = String.Empty;
        }
    }
}
