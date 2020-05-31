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
    /// Interaction logic for EditarProductoIngrediente.xaml
    /// </summary>
    public partial class EditarProductoIngrediente : Window
    {
        public EditarProductoIngrediente(Producto producto)
        {
            InitializeComponent();
            productoCargar = producto;
            CargarCampos();
        }

        Producto productoCargar = new Producto();



        private void CargarCampos()
        {
            ProductoIngredienteController productoIngredienteController = new ProductoIngredienteController();
            ProductoIngrediente productoIngrediente = productoIngredienteController.buscarProductoIngredientePorId(productoCargar.idProducto);
            txb_nombre.Text = productoIngrediente.Nombre;
            txb_codigo.Text = productoIngrediente.Código;
            txb_descripcion.Text = productoIngrediente.Descripción;
            txb_Restricción.Text = productoIngrediente.Restricción;

        }

    }
}
