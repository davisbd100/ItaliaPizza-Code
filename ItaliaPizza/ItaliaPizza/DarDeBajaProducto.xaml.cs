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
        public DarDeBajaProducto()
        {
            InitializeComponent();
            BuscarProducto();
        }

        private string TIPO_PRODUCTO = "";

        private void BuscarProducto()
        {
            ProductoVentaController productoVentaController = new ProductoVentaController();
            BusinessLogic.ProductoVenta productoVenta = productoVentaController.BuscarProductoVenta(3123123);

            if(productoVenta.Nombre != null)
            {
                TIPO_PRODUCTO = "Venta";
            }
            else
            {
                TIPO_PRODUCTO = "Ingrediente";
            }
        }

        private void EliminarProductoVenta()
        {

        }



    }
}
