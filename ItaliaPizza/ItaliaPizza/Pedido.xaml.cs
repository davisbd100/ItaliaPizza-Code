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
using Controller;

namespace PrototiposItaliaPizza
{
    /// <summary>
    /// Lógica de interacción para Pedido.xaml
    /// </summary>
    public partial class Pedido : Window
    {
        InventarioController controller = new InventarioController();
        public Pedido()
        {
            InitializeComponent();

        }

        private void btCerrarDia_Click(object sender, RoutedEventArgs e)
        {
            controller.ActualizarExistencias(controller.ObtenerInventario());
        }
    }
}
