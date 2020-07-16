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
    }
}
