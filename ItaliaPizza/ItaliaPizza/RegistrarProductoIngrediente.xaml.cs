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
    /// Interaction logic for RegistrarProducto.xaml
    /// </summary>
    public partial class RegistrarProducto : Window
    {
        public RegistrarProducto()
        {
            InitializeComponent();
            LlenarTipoIngrediente();
        }
        private enum CheckResult
        {
            Passed,
            Failed
        }


        private void LlenarTipoIngrediente()
        {

            cbb_TipoIngrediente.ItemsSource = Enum.GetValues(typeof(TipoIngredienteEnum));
        }

        private bool VerificarCamposVacios()
        {
            bool resultado = true;

            if (txb_Nombre.Text == String.Empty || txb_Codigo.Text == String.Empty || txb_Descripcion.Text == String.Empty
                || txb_Preciounitario.Text == String.Empty || txb_Restricción.Text == String.Empty || txb_UnidadMedida.Text == String.Empty
                || txb_Ubicacion.Text == String.Empty || txb_Cantidad.Text == String.Empty || dtp_Caducidad.Text ==  String.Empty )
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
            else if (validarCampos.ValidarNumeroEntero(txb_Cantidad.Text) == ValidarCampos.ResultadosValidación.Numeroinvalido)
            {
                MessageBox.Show("Codigo y cantidad deben ser números enteros");
                resultado = false;
            }
            else if (validarCampos.ValidarNumeroFloat(txb_Preciounitario.Text) == ValidarCampos.ResultadosValidación.Numeroinvalido)
            {
                MessageBox.Show("Los precios deben ser solo números");
                resultado = false;
            }

           return resultado;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (VerificarCampos())
            {
                RegistrarProductoingrediente();

            }


        }

        


        private void RegistrarProductoingrediente()
        {
            ProductoIngredienteController productoIngredienteController = new ProductoIngredienteController();
            

            if (productoIngredienteController.crearProductoIngrediente(txb_Nombre.Text, txb_Codigo.Text, txb_Descripcion.Text, float.Parse(txb_Preciounitario.Text), 
                txb_Restricción.Text, txb_UnidadMedida.Text, txb_Ubicacion.Text,
                Convert.ToInt32(txb_Cantidad.Text), dtp_Caducidad.SelectedDate.Value.ToString("yyyy/MM/dd") , cbb_TipoIngrediente.SelectedItem.ToString() ) == BusinessLogic.ResultadoOperacionEnum.ResultadoOperacion.Exito)
            {
                MessageBox.Show("Producto registrado con éxito");
            }
            else
            {
                MessageBox.Show("No se pudo registrar el producto");
            }
        }



    }
}
