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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ItaliaPizza
{
    /// <summary>
    /// Lógica de interacción para UserControlSalir.xaml
    /// </summary>
    public partial class UserControlSalir : UserControl
    {
        public UserControlSalir()
        {
            InitializeComponent();
        }

        private void SalirButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Está seguro que desea Salir?", "Salir", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Inicio inicio = new Inicio();
                    abrirVentana(inicio);
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }

        public void abrirVentana(Window window)
        {
            window.Show();
            var myWindow = Window.GetWindow(this);
            myWindow.Close();
        }
    }
}
