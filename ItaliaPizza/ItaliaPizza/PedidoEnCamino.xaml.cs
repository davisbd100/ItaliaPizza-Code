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
using BusinessLogic;
using Controller;

namespace ItaliaPizza
{
    /// <summary>
    /// Lógica de interacción para DarDeBajaCliente.xaml
    /// </summary>
    public partial class PedidoEnCamino : Window
    {

        BusinessLogic.Pedido LocalPedido { get; set; }
        public PedidoEnCamino()
        {
            LocalPedido = new BusinessLogic.Pedido()
            {
                idPedido = 1
            };
            InitializeComponent();
        }        
        public PedidoEnCamino(int idPedido)
        {
            LocalPedido = new BusinessLogic.Pedido()
            {
                idPedido = idPedido
            };
            InitializeComponent();
        }

        private void btAceptar_Click(object sender, RoutedEventArgs e)
        {
            PedidoController controller = new PedidoController();
            if (tbRepartidor.Text == "")
            {
                MessageBox.Show("Debes asignar un nombre");
                return;
            }
            ResultadoOperacionEnum.ResultadoOperacion resultado = controller.AsignarEntrega(LocalPedido.idPedido, tbRepartidor.Text);

            if (resultado == ResultadoOperacionEnum.ResultadoOperacion.Exito)
            {
                MessageBox.Show("Pedido En Camino :3");
            }else if(resultado == ResultadoOperacionEnum.ResultadoOperacion.FalloSQL)
            {
                MessageBox.Show("Error con la base de datos, reintentar mas tarde");
            }
            this.Close();
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
