using Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Lógica de interacción para Empleados.xaml
    /// </summary>
    public partial class InventarioFinal : Window
    {
        List<DataAccess.Inventario> inventario = new List<DataAccess.Inventario>();
        InventarioController controller = new InventarioController();
        public InventarioFinal()
        {
            InitializeComponent();
            inventario = controller.ObtenerInventario();
            if (!inventario.Any())
            {
                MessageBox.Show("No se tienen productos registrados");
                this.Close();
            }
            else
            {
                dgInventario.ItemsSource = inventario;
            }
        }


        private void btBusqueda_Click(object sender, RoutedEventArgs e)
        {
            string filter = tbBusqueda.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(dgInventario.ItemsSource);
            if (filter == "")
                cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    DataAccess.Inventario p = o as DataAccess.Inventario;
                    if (tbBusqueda.Name == "tbBusqueda")
                        return (p.Producto1.Nombre == filter);
                    return (p.Producto1.Nombre.ToUpper().StartsWith(filter.ToUpper()));
                };
            }
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btAceptar_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
