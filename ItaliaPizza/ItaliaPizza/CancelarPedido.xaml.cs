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
    public partial class CancelarPedido : Window
    {

        BusinessLogic.Pedido LocalPedido { get; set; }
        public bool Flag { get; set; } = false;   
        public CancelarPedido(int idPedido)
        {
            LocalPedido = new BusinessLogic.Pedido()
            {
                idPedido = idPedido
            };
            InitializeComponent();
        }
        public CancelarPedido(int idPedido, bool flag)
        {
            LocalPedido = new BusinessLogic.Pedido()
            {
                idPedido = idPedido
            };
            Flag = flag;
            InitializeComponent();
            lbMessage.Content = "El pedido ya no se puede cancelar,\n por lo que se marcara como no entregado, \n ¿Desea continuar?";
        }

        private void btAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (Flag)
            {
                PedidoController controller = new PedidoController();
                ResultadoOperacionEnum.ResultadoOperacion resultado = controller.CancelarPedido(LocalPedido);
                if (resultado == ResultadoOperacionEnum.ResultadoOperacion.Exito)
                {
                    MessageBox.Show("Pedido Cancelado");
                }
                else if (resultado == ResultadoOperacionEnum.ResultadoOperacion.FalloSQL)
                {
                    MessageBox.Show("Error con la base de datos, reintentar mas tarde");
                }
                this.Close();
            }
            else
            {
                PedidoController controller = new PedidoController();
                ResultadoOperacionEnum.ResultadoOperacion resultado = controller.CancelarPedido(LocalPedido);
                if (resultado == ResultadoOperacionEnum.ResultadoOperacion.Exito)
                {
                    MessageBox.Show("Pedido Cancelado");
                }
                else if (resultado == ResultadoOperacionEnum.ResultadoOperacion.FalloSQL)
                {
                    MessageBox.Show("Error con la base de datos, reintentar mas tarde");
                }
                this.Close();
            }
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
