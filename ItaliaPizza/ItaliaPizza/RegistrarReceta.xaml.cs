﻿using BusinessLogic;
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
            dtg_IngredientesReceta.ItemsSource = listaIngredinetes;

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
                Console.WriteLine(listaIngredinetes[0].idProducto);
                var ingredienteSeleccionado = dtg_IngredientesReceta.SelectedItem as Producto;
                listaIngredinetes.RemoveAll(r => r.idProducto == ingredienteSeleccionado.idProducto);
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
            var resultado = recetaController.CrearReceta(txb_Nombre.Text, txb_Procedimiento.Text, txb_Rendimiento.Text, listaIngredinetes, productoMandado.idProducto);
            if(resultado == ResultadoOperacionEnum.ResultadoOperacion.Exito)
            {
                MessageBox.Show("Receta registrada con éxito");
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
    }
}
