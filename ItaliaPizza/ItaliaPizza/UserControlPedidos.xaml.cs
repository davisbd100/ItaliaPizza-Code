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
    /// Lógica de interacción para UserControlPedidos.xaml
    /// </summary>
    public partial class UserControlPedidos : UserControl
    {
        public UserControlPedidos()
        {
            InitializeComponent();
            LabelNombre.Content = Properties.Settings.Default.NombreEmpleado;
            LabelTipoEmpleado.Content = Properties.Settings.Default.EmpleadoType;
        }
    }
}
