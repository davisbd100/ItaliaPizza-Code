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
    /// Interaction logic for RegistrarProducto.xaml
    /// </summary>
    public partial class RegistrarProducto : Window
    {
        public RegistrarProducto()
        {
            InitializeComponent();
        }
        private enum CheckResult
        {
            Passed,
            Failed
        }

        private bool VerificarCamposVacios()
        {
            bool resultado = true;

            if (txb_Nombre.Text == String.Empty || txb_Codigo.Text == String.Empty || txb_Descripcion.Text == String.Empty
                || txb_Preciounitario.Text == String.Empty || txb_Restricción.Text == String.Empty || txb_UnidadMedida.Text == String.Empty
                || txb_Ubicacion.Text == String.Empty || txb_Cantidad.Text == String.Empty || txb_Caducidad.Text == String.Empty )
            {
                resultado = false;
            }

            return resultado;
        }

        private bool VerificarCampos()
        {
            bool resultado = true;
            ValidarCampos validarCampos = new ValidarCampos();
            if (!VerificarCamposVacios())
            {
                MessageBox.Show("Existen campos vacios");
                resultado = false;
            }
            else if (validarCampos.ValidarNumeroEntero(txb_Codigo.Text) == ValidarCampos.ResultadosValidación.Numeroinvalido
                || validarCampos.ValidarNumeroEntero(txb_Cantidad.Text) == ValidarCampos.ResultadosValidación.Numeroinvalido)
            {
                MessageBox.Show("Codigo y cantidad deben ser números enteros");
                resultado = false;
            }
            else if (validarCampos.ValidarNumeroFloat(txb_Preciounitario.Text) == ValidarCampos.ResultadosValidación.Numeroinvalido ||
                validarCampos.ValidarNumeroFloat(txb_PrecioVenta.Text) == ValidarCampos.ResultadosValidación.Numeroinvalido)
            {
                MessageBox.Show("Los precios deben ser solo números");
                resultado = false;
            }

           return resultado;
        }

        private int VerificarTipoIngrediente()
        {
            int index = Convert.ToInt32(cbb_TipoIngrediente.SelectedIndex.ToString());
            Console.WriteLine(index);

            if (index == -1)
            {
               MessageBox.Show("Debe ingresar el tipo de ingrediente");
            }
            return 1;
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int tipoIngrediente = VerificarTipoIngrediente();
            if (VerificarCampos())
            {
                if(tipoIngrediente == 0)
                {

                }else if (tipoIngrediente == 1)
                {
                    RegistrarProductoVenta();
                }

            }
        }

        private void RegistrarProductoVenta()
        {
            ProductoVentaController productoVentaController = new ProductoVentaController();
            bool requiereReceta = false;
            if (cb_RequiereReceta.IsChecked.Value)
            {
                requiereReceta = true;
            }

            if (productoVentaController.crearProducto(txb_Nombre.Text, Convert.ToInt32( txb_Codigo.Text), txb_Descripcion.Text, float.Parse( txb_Preciounitario.Text), 
                txb_Restricción.Text, txb_UnidadMedida.Text, float.Parse(txb_PrecioVenta.Text),requiereReceta, "url de la foto",txb_Ubicacion.Text, 
                Convert.ToInt32(txb_Cantidad.Text) ,txb_Caducidad.Text, "Example" ) == BusinessLogic.ResultadoOperacionEnum.ResultadoOperacion.Exito)
            {
                MessageBox.Show("Producto registrado con éxito");
            }
            else
            {
                MessageBox.Show("ocurrió un error");
            }
        }

        private void RegistrarProductoingrediente()
        {

        }



    }
}
