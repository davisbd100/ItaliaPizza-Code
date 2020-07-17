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
    /// Interaction logic for RecetasGerente.xaml
    /// </summary>
    public partial class RecetasGerente : Window
    {
        public RecetasGerente()
        {
            InitializeComponent();
            llenarGrid();
        }
        List<Receta> recetas = new List<Receta>();
        const int POSICION_FUERA_RANGO = -1;



        private bool ValidarSeleccion()
        {
            bool resultado = false;
            if (dtg_Recetas.SelectedItems.Count == 1)
            {
                resultado = true;
            }

            return resultado;
        }

        private void llenarGrid()
        {
            dtg_Recetas.ItemsSource = null;
            RecetaController recetaController = new RecetaController();
            recetas = recetaController.ObtenerTodasRecetas(1);
            dtg_Recetas.ItemsSource = recetas;

        }

        private void btn_Nuevo_Click(object sender, RoutedEventArgs e)
        {
            RegistrarRecetaSeleccionProducto producto = new RegistrarRecetaSeleccionProducto();
            producto.Show();
        }

        private void SalirButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void VerReceta_Click(object sender, RoutedEventArgs e)
        {
            int posicion = dtg_Recetas.SelectedIndex;


            if (posicion != POSICION_FUERA_RANGO && ValidarSeleccion())
            {
                Receta receta = (Receta)dtg_Recetas.SelectedItem;

                VerReceta verReceta = new VerReceta(receta);
                verReceta.Show();
            }
        }
    }
}
