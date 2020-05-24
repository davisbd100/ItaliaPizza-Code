using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
