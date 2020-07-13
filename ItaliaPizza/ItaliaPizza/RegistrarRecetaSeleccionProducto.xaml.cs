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

namespace ItaliaPizza
{
    /// <summary>
    /// Interaction logic for RegistrarRecetaSeleccionProducto.xaml
    /// </summary>
    public partial class RegistrarRecetaSeleccionProducto : Window
    {
        public RegistrarRecetaSeleccionProducto()
        {
            InitializeComponent();
            LlenarGridProductoVenta();
        }

        const int POSICION_FUERA_RANGO = -1;



        private void LlenarGridProductoVenta()
        {
            dtg_Productos.ItemsSource = null;
            ProductoVentaController productoVentaController = new ProductoVentaController();
            List<Producto> productos = new List<Producto>();
            List<ProductoVenta> listaProductosVenta = productoVentaController.ObtenerProductoVentaSinRecetaAsignada(1);
            productos.AddRange(listaProductosVenta);
            dtg_Productos.ItemsSource = productos;
        }

        private bool ValidarSeleccion()
        {
            bool resultado = false;
            if (dtg_Productos.SelectedItems.Count == 1)
            {
                resultado = true;
            }

            return resultado;
        }

        private void btn_Seleccionar_Click(object sender, RoutedEventArgs e)
        {
            int posicion = dtg_Productos.SelectedIndex;

            if (posicion != POSICION_FUERA_RANGO && ValidarSeleccion())
            {
                ProductoVenta producto = (ProductoVenta)dtg_Productos.SelectedItem;

                {
                    RegistrarReceta registrarReceta = new RegistrarReceta(producto);
                    registrarReceta.Show();
                }

            }
        }
    }
}
