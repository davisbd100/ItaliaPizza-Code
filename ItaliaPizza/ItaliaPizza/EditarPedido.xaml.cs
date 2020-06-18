﻿using BusinessLogic;
using Controller;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Lógica de interacción para EditarCliente.xaml
    /// </summary>
    public partial class EditarPedido : Window
    {
        int PedidoID { get; set; }
        public DataAccess.Pedido PedidoAEditar;
        PedidoController controller = new PedidoController();
        public EditarPedido()
        {
            InitializeComponent();
            PedidoID = 1;
            ObtenerPedido();
        }
        public EditarPedido(int id)
        {
            InitializeComponent();
            PedidoID = id;
            ObtenerPedido();
        }

        void ObtenerPedido()
        {
            try
            {
                PedidoAEditar = controller.ObtenerPedidoParaEditar(PedidoID);
            }
            catch (EntityException)
            {
                MessageBox.Show("No se pudo obtener el pedido para la edicion \nReintentar mas tarde");
                this.Close();
            }
            catch (InstanceNotFoundException)
            {
                MessageBox.Show("No se encontro el pedido, verificar la existencia");
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("El pedido no esta en espera y ya no es posible modificarlo");
                this.Close();
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ActualizarDataGrid();
            lbEstatus.Content = PedidoAEditar.Estatus1.NombreEstatus;
            lbIdCliente.Content = PedidoAEditar.Cliente;
            lbIdPedido.Content = PedidoAEditar.idPedido;
            double CostoTotal = 0;
            lbNuevoPrecio.Content = String.Format("{0:0.00}", CostoTotal) + "  MXN";


            foreach (var pedidoProducto in PedidoAEditar.PedidoProducto)
            {
                CostoTotal += (double)pedidoProducto.Precio;
            }
            lbPrecioAnterior.Content = String.Format("{0:0.00}", CostoTotal) + "  MXN";
        }

        private void ActualizarDataGrid()
        {
            dgProductosDePedido.ItemsSource = null;
            dgProductosDePedido.ItemsSource = PedidoAEditar.PedidoProducto;
        }

        private void Quitar_Click(object sender, RoutedEventArgs e)
        {
            if (dgProductosDePedido.SelectedIndex != -1)
            {
                PedidoAEditar.PedidoProducto.Remove((DataAccess.PedidoProducto)dgProductosDePedido.SelectedItem);
                ActualizarDataGrid();
            }
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btEditar_Click(object sender, RoutedEventArgs e)
        {
            GuardarEdicionPedido();
        }

        private void GuardarEdicionPedido()
        {
            switch(controller.CambiarProductosPedido(PedidoAEditar.idPedido, PedidoAEditar.PedidoProducto))
            {
                case (ResultadoOperacionEnum.ResultadoOperacion.Exito):
                    MessageBox.Show("Guardado con exito");
                    this.Close();
                    break;
                case (ResultadoOperacionEnum.ResultadoOperacion.FallaDesconocida):
                    MessageBox.Show("Fallo no esperado, reintentar mas tarde");
                    break;
                case (ResultadoOperacionEnum.ResultadoOperacion.FalloSQL):
                    MessageBox.Show("Error al conectar con la base de datos, reintentar mas tarde");
                    break;
            }
        }

        private void ProductosUC_ProductoUserControlClicked(object sender, EventArgs e)
        {
            ProductoVenta tempProducto = ((ProductoVenta)sender);
            ProductoVentaController producto = new ProductoVentaController();
            int cantidad = 1;
            DataAccess.PedidoProducto tempPedidoProducto = new DataAccess.PedidoProducto()
            {
                Cantidad = cantidad,
                idPedido = PedidoAEditar.idPedido,
                ProductoVenta = producto.ObtenerProductoPorIdEE(int.Parse(tempProducto.Código)),
                Precio = cantidad * tempProducto.PrecioPúblico
            };
            var match = Regex.Match(lbNuevoPrecio.Content.ToString(), @"([-+]?[0-9]*\.?[0-9]+)");
            double oldNuevoPrecio = -1;
            if (match.Success)
            {
                oldNuevoPrecio = Convert.ToDouble(match.Groups[1].Value);
            }
            if (oldNuevoPrecio == -1)
            {
                MessageBox.Show("No se pudo obtener el producto");
                return;
            }
            else
            {
                double nuevoPrecio = oldNuevoPrecio + (double)tempPedidoProducto.Precio;
                lbNuevoPrecio.Content = String.Format("{0:0.00}", nuevoPrecio) + "  MXN";
            }
            PedidoAEditar.PedidoProducto.Add(tempPedidoProducto);
            ActualizarDataGrid();
        }
    }
}
