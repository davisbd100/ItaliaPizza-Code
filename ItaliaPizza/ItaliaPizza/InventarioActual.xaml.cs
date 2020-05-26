using Controller;
using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            controller.ActualizarExistencias(inventario);
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
                    Inventario p = o as Inventario;
                    if (tbBusqueda.Name == "tbBusqueda")
                        return (p.Producto1.Nombre == filter);
                    return (p.Producto1.Nombre.ToUpper().StartsWith(filter.ToUpper()));
                };
            }
        }
    }
}
