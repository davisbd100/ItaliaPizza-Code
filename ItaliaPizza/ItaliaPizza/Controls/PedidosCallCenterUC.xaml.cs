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
    public partial class PedidosCallCenterUC : UserControl
    {
        public event EventHandler PedidosCallCenterUserControlClicked;
        PedidoController Controller = new PedidoController();
        List<DataAccess.Pedido> pedidos = new List<DataAccess.Pedido>();
        public PedidosCallCenterUC()
        {
            InitializeComponent();
            UpdateGrid();
        }

        public void UpdateGrid()
        {
            pedidos = Controller.ObtenerPedidosCallCenter();
            icPedidos.ItemsSource = null;
            icPedidos.ItemsSource = pedidos;
            Console.WriteLine(icPedidos.ItemsSource);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Loaded");
        }


        private void btContent_Click(object sender, RoutedEventArgs e)
        {
            PedidosCallCenterUserControlClicked?.Invoke(((Button)sender).DataContext, EventArgs.Empty);
        }

        private void cbTiposDePedido_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO
        }
    }
}
