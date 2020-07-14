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
using Controller;
using BusinessLogic;

namespace ItaliaPizza.Controls
{
    /// <summary>
    /// Interaction logic for PedidosUC.xaml
    /// </summary>
    public partial class PedidosUC : UserControl
    {
        public event EventHandler PedidoUserControlClicked;
        PedidoController Controller = new PedidoController();
        List<DataAccess.Pedido> Pedidos = new List<DataAccess.Pedido>();
        public PedidosUC()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Loaded");
            Pedidos = Controller.ObtenerPedidosCocina();
        }


        private void btContent_Click(object sender, RoutedEventArgs e)
        {
            PedidoUserControlClicked?.Invoke(((Button)sender).DataContext, EventArgs.Empty);
        }
    }
}
