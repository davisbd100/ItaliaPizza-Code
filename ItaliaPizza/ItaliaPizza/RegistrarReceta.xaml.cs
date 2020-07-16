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
    /// Interaction logic for RegistrarReceta.xaml
    /// </summary>
    public partial class RegistrarReceta : Window
    {
        public RegistrarReceta(Producto producto)
        {
            InitializeComponent();
            LlenarGridIngrediente();
            productoMandado = producto;
        }

        public RegistrarReceta()
        {
            InitializeComponent();
            LlenarGridIngrediente();
        }

        Producto productoMandado = new Producto();
        const int POSICION_FUERA_RANGO = -1;
        List<ProductoIngrediente> listaIngredinetes = new List<ProductoIngrediente>();
        List<ListaIngredientesReceta> listaIngredientesRecetas = new List<ListaIngredientesReceta>();

        private void LlenarGridIngrediente()
        {
            dtg_Ingredientes.ItemsSource = null;
            ProductoIngredienteController productoIngredienteController = new ProductoIngredienteController();
            List<Producto> productos = new List<Producto>();
            List<ProductoIngrediente> listaProductosIngrediente = productoIngredienteController.ObtenerProductosIngrediente(Convert.ToInt32(/*txb_Pagina.Text.ToString()*/ 1 ));
            productos.AddRange(listaProductosIngrediente);
            dtg_Ingredientes.ItemsSource = productos;
        }

        private void LlenarGridIngredienteReceta()
        {
            dtg_IngredientesReceta.ItemsSource = null;

            List<ListaIngredientesReceta> listaIngredientesRecetas2 = new List<ListaIngredientesReceta>();

            foreach (ProductoIngrediente ingrediente in listaIngredinetes)
            {
                ListaIngredientesReceta ingredientesReceta = new ListaIngredientesReceta();

                ingredientesReceta.IdIngrediente = ingrediente.idProducto;
                ingredientesReceta.Nombre = ingrediente.Nombre;
                ingredientesReceta.Descripción = ingrediente.Descripción;
                ingredientesReceta.Cantidad = 1;
                ingredientesReceta.Código = ingrediente.Código;
                ingredientesReceta.PrecioUnitario = 0;
                listaIngredientesRecetas2.Add(ingredientesReceta);
                listaIngredientesRecetas = listaIngredientesRecetas2;
            }


            dtg_IngredientesReceta.ItemsSource = listaIngredientesRecetas;

        }

        private void actualizarGrid()
        {

            dtg_IngredientesReceta.ItemsSource = null;
            dtg_IngredientesReceta.ItemsSource = listaIngredientesRecetas;

        }

        private bool ValidarSeleccion()
        {
            bool resultado = false;
            if (dtg_Ingredientes.SelectedItems.Count == 1)
            {
                resultado = true;
            }

            return resultado;
        }

        private bool ValidarSeleccionReceta()
        {
            bool resultado = false;
            if (dtg_IngredientesReceta.SelectedItems.Count == 1)
            {
                resultado = true;
            }

            return resultado;
        }

        private void btn_agregarAGrid_Click(object sender, RoutedEventArgs e)
        {
            int posicion = dtg_Ingredientes.SelectedIndex;

            if (posicion != POSICION_FUERA_RANGO && ValidarSeleccion())
            {

                var ingredienteSeleccionado = dtg_Ingredientes.SelectedItem as ProductoIngrediente;


                listaIngredinetes.Add(ingredienteSeleccionado);

                LlenarGridIngredienteReceta();
            }
        }

        private void btn_ElimiarDeGrid_Click(object sender, RoutedEventArgs e)
        {
            int posicion = dtg_IngredientesReceta.SelectedIndex;

            if(posicion != POSICION_FUERA_RANGO && ValidarSeleccionReceta())
            {
                var ingredienteSeleccionado = dtg_IngredientesReceta.SelectedItem as ListaIngredientesReceta;
                listaIngredinetes.RemoveAll(r => r.idProducto == ingredienteSeleccionado.IdIngrediente);

                //foreach (ProductoIngrediente productoIngrediente in listaIngredinetes)
                //{
                //    if(productoIngrediente.idProducto == ingredienteSeleccionado.IdIngrediente)
                //    {
                //        listaIngredinetes.Remove(productoIngrediente);
                //    }
                //}

                LlenarGridIngredienteReceta();

            }

        }

        private bool VerificarCampos()
        {
            bool resultado = true;
            if(txb_Nombre.Text == string.Empty && txb_Procedimiento.Text == string.Empty && txb_Rendimiento.Text == string.Empty)
            {
                resultado = false;
            }

            return resultado;
        }

        private void RegistarReceta()
        {
            RecetaController recetaController = new RecetaController();
            var resultado = recetaController.CrearReceta(txb_Nombre.Text, txb_Procedimiento.Text, txb_Rendimiento.Text, listaIngredientesRecetas, productoMandado.idProducto);
            if(resultado == ResultadoOperacionEnum.ResultadoOperacion.Exito)
            {
                MessageBox.Show("Receta registrada con éxito");
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo registrar la receta");
            }
             
        }

        private void btn_Registrar_Click(object sender, RoutedEventArgs e)
        {


            if (VerificarCampos())
            {
                RegistarReceta();
            }
            else
            {
                MessageBox.Show("Hay campos vacios, verifique su información");
            }
        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Está seguro de que desea cancelar?", "Cancelar", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Close();
                    break;
                case MessageBoxResult.No:
                    RegistrarEmpleado registrarEmpleado = new RegistrarEmpleado();
                    registrarEmpleado.Close();
                    break;
            }
        }
    }
}
