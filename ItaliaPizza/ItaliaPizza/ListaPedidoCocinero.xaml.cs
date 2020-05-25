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
    /// Lógica de interacción para Pedido.xaml
    /// </summary>
    public partial class ListaPedidoCocinero : Window
    {
        public List<DataAccess.Pedido> pedidos { get; set; }
        PedidoController PedidoController = new PedidoController();
        public ListaPedidoCocinero()
        {
            InitializeComponent();
            pedidos = PedidoController.ObtenerPedidosCocina();
        }
    }
}
