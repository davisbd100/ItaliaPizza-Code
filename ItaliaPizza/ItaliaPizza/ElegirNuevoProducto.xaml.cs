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

namespace ItaliaPizza
{
    /// <summary>
    /// Interaction logic for ElegirNuevoProducto.xaml
    /// </summary>
    public partial class ElegirNuevoProducto : Window
    {
        public ElegirNuevoProducto()
        {
            InitializeComponent();
        }

        private void btn_cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_ingredinete_Click(object sender, RoutedEventArgs e)
        {
            RegistrarProducto registrarProductoIngrediente = new RegistrarProducto();
            registrarProductoIngrediente.Show();
            this.Close();
        }

        private void btn_Venta_Click(object sender, RoutedEventArgs e)
        {
            RegistrarProductoVenta registrarProductoVenta = new RegistrarProductoVenta();
            registrarProductoVenta.Show();
            this.Close();
        }
    }
}
