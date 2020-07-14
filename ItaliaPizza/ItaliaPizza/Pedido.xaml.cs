using System.Windows;
using Controller;

namespace ItaliaPizza
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
            controller.CerrarDia();
            this.Close();
        }
    }
}
