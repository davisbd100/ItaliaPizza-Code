using Controller;
using DataAccess;
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

namespace ItaliaPizza
{
    /// <summary>
    /// Interaction logic for DarDeBajaProducto.xaml
    /// </summary>
    public partial class DarDeBajaProducto : Window
    {
        public DarDeBajaProducto(BusinessLogic.Producto productoPasado, string tipo)
        {
            InitializeComponent();
            TipoProducto = tipo;
            producto = productoPasado;
        }

        BusinessLogic.Producto producto = new BusinessLogic.Producto();
        string TipoProducto = "";


        private void EliminarProductoventa()
        {
            ProductoVentaController productoVentaController = new ProductoVentaController();
            if(productoVentaController.EliminarproductoVenta(producto) == ResultadoOperacionEnum.ResultadoOperacion.Exito)
            {
                MessageBox.Show("Producto eliminado con éxito");
            }
            else
            {
                MessageBox.Show("No se pudo eliminar el producto");
            }
        }

        private void EliminarProductoIngrediente()
        {
            ProductoIngredienteController productoIngredienteController = new ProductoIngredienteController();
            if(productoIngredienteController.EliminarProductoIngrediente(producto) == ResultadoOperacionEnum.ResultadoOperacion.Exito)
            {
                MessageBox.Show("Producto eliminado con éxito");
            }
            else
            {
                MessageBox.Show("No se pudo eliminar el producto");
            }
        }

        private void btn_aceptar_Click(object sender, RoutedEventArgs e)
        {
            if(TipoProducto == "Venta")
            {
                EliminarProductoventa();
            }
            else if(TipoProducto == "Ingrediente")
            {
                EliminarProductoIngrediente();
            }

            else
            {
                MessageBox.Show("producto desconocido");
                this.Close();
            }
        }

        private void btn_cancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
