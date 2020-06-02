using BusinessLogic;
using Controller;
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

namespace PrototiposItaliaPizza
{
    /// <summary>
    /// Lógica de interacción para EditarCliente.xaml
    /// </summary>
    public partial class EditarPedido : Window
    {
        int PedidoID { get; set; }
        DataAccess.Pedido PedidoAEditar;
        PedidoController controller = new PedidoController();
        public EditarPedido()
        {
            InitializeComponent();
            PedidoID = 1;
            PedidoAEditar = controller.ObtenerPedidoParaEditar(1);
        }
        public EditarPedido(int id)
        {
            InitializeComponent();
            PedidoID = id;
            PedidoAEditar = controller.ObtenerPedidoParaEditar(id);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
