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
    /// Interaction logic for EditarProductoVenta.xaml
    /// </summary>
    public partial class EditarProductoVenta : Window
    {
        public EditarProductoVenta(Producto producto)
        {
            InitializeComponent();
            productoCargar = producto;
            CargarCampos();
        }

        Producto productoCargar = new Producto();

        

        private void CargarCampos()
        {
            ProductoVentaController productoVentaController = new ProductoVentaController();
            ProductoVenta productoVenta = productoVentaController.BuscarProductoVenta(productoCargar.idProducto);
            txb_nombre.Text = productoVenta.Nombre;
            txb_codigo.Text = productoVenta.Código;
            txb_descripcion.Text = productoVenta.Descripción;
            txb_restriccion.Text = productoVenta.Restricción;
            txb_precioPublico.Text = productoVenta.PrecioPúblico.ToString();

        }

        private void ButtonCancelar_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Close();
                    break;
                case MessageBoxResult.No:
                    RegistrarCliente registrarCliente = new RegistrarCliente();
                    registrarCliente.Close();
                    break;
            }
        }
    }
}
