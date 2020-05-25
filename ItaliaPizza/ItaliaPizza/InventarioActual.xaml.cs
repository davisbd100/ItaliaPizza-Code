using Controller;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace ItaliaPizza
{
    /// <summary>
    /// Lógica de interacción para Empleados.xaml
    /// </summary>
    public partial class InventarioActual : Window
    {
        List<DataAccess.Inventario> inventario = new List<DataAccess.Inventario>();
        InventarioController controller = new InventarioController();
        public InventarioActual()
        {
            InitializeComponent();
            inventario = controller.ObtenerInventario();
            dgInventario.ItemsSource = inventario;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
