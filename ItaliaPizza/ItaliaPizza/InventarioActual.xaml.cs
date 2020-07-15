using Controller;
using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace ItaliaPizza
{
    /// <summary>
    /// Lógica de interacción para Empleados.xaml
    /// </summary>
    public partial class InventarioActual : Window
    {
        private class CustomInventario : DataAccess.Inventario
        {
            public String CodigoProducto { get; set; }
        }
        List<CustomInventario> inventario = new List<CustomInventario>();
        InventarioController controller = new InventarioController();
        int PaginaActual = 1;
        int PaginaTotal = 1;

        public InventarioActual()
        {
            InitializeComponent();
            inventario.Clear();
            foreach (var item in controller.ObtenerInventarioPorRango(PaginaActual))
            {
                CustomInventario customInventario = new CustomInventario()
                {
                    ExistenciaInicial = item.ExistenciaInicial,
                    ExistenciaTotal = item.ExistenciaTotal,
                    idInventario = item.idInventario,
                    Producto = item.Producto,
                    UnidadMedida = item.UnidadMedida
                };
                ProductoController productoController = new ProductoController();
                DataAccess.Producto producto = new DataAccess.Producto();
                customInventario.CodigoProducto = productoController.ObtenerProductoPorId((int)customInventario.Producto).Codigo;
                inventario.Add(customInventario);
            } 
            if (!inventario.Any())
            {
                MessageBox.Show("No se tienen productos registrados");
                this.Close();
            }
            else
            {
                tbPaginaActual.Text = PaginaActual.ToString();
                PaginaTotal = controller.ObtenerPaginasDeTablaInventario();
                tbPaginaTotal.Text = PaginaTotal.ToString();
                dgInventario.ItemsSource = inventario;
            }
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            controller.ActualizarExistencias(inventario.ConvertAll(b=> (DataAccess.Inventario)b));
            MessageBox.Show("Inventario actualizado");
            inventario.Clear();
            foreach (var item in controller.ObtenerInventarioPorRango(PaginaActual))
            {
                CustomInventario customInventario = new CustomInventario()
                {
                    ExistenciaInicial = item.ExistenciaInicial,
                    ExistenciaTotal = item.ExistenciaTotal,
                    idInventario = item.idInventario,
                    Producto = item.Producto,
                    UnidadMedida = item.UnidadMedida
                };
                ProductoController productoController = new ProductoController();
                DataAccess.Producto producto = new DataAccess.Producto();
                customInventario.CodigoProducto = productoController.ObtenerProductoPorId((int)customInventario.Producto).Codigo;
                inventario.Add(customInventario);
            }
            tbPaginaActual.Text = PaginaActual.ToString();
            PaginaTotal = controller.ObtenerPaginasDeTablaInventario();
            tbPaginaTotal.Text = PaginaTotal.ToString();
            dgInventario.ItemsSource = null;
            dgInventario.ItemsSource = inventario;
        }

        private void btBusqueda_Click(object sender, RoutedEventArgs e)
        {
            string filter = tbBusqueda.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(dgInventario.ItemsSource);
            if (filter == "")
                cv.Filter = null;
            else
            {
                cv.Filter = o =>
                {
                    CustomInventario p = o as CustomInventario;
                    if (tbBusqueda.Name == "tbBusqueda")
                        return (p.CodigoProducto == filter);
                    return (p.CodigoProducto.ToUpper().StartsWith(filter.ToUpper()));
                };
            }
        }

        private void btPaginaAnterior_Click(object sender, RoutedEventArgs e)
        {
            if ((PaginaActual - 1) < 1)
            {
                MessageBox.Show("No se puede regresar mas");
            }
            else
            {
                PaginaActual--;
                inventario.Clear();
                foreach (var item in controller.ObtenerInventarioPorRango(PaginaActual))
                {
                    CustomInventario customInventario = new CustomInventario()
                    {
                        ExistenciaInicial = item.ExistenciaInicial,
                        ExistenciaTotal = item.ExistenciaTotal,
                        idInventario = item.idInventario,
                        Producto = item.Producto,
                        UnidadMedida = item.UnidadMedida
                    };
                    ProductoController productoController = new ProductoController();
                    DataAccess.Producto producto = new DataAccess.Producto();
                    customInventario.CodigoProducto = productoController.ObtenerProductoPorId((int)customInventario.Producto).Codigo;
                    inventario.Add(customInventario);
                }
                tbPaginaActual.Text = PaginaActual.ToString();
                dgInventario.ItemsSource = null;
                dgInventario.ItemsSource = inventario;

            }
        }

        private void btPaginaSiguiente_Click(object sender, RoutedEventArgs e)
        {

            if (PaginaActual == PaginaTotal)
            {
                MessageBox.Show("No se puede avanzar mas");
            }
            else
            {
                PaginaActual++;
                inventario.Clear();
                foreach (var item in controller.ObtenerInventarioPorRango(PaginaActual))
                {
                    CustomInventario customInventario = new CustomInventario()
                    {
                        ExistenciaInicial = item.ExistenciaInicial,
                        ExistenciaTotal = item.ExistenciaTotal,
                        idInventario = item.idInventario,
                        Producto = item.Producto,
                        UnidadMedida = item.UnidadMedida
                    };
                    ProductoController productoController = new ProductoController();
                    DataAccess.Producto producto = new DataAccess.Producto();
                    customInventario.CodigoProducto = productoController.ObtenerProductoPorId((int)customInventario.Producto).Codigo;
                    inventario.Add(customInventario);
                }
                tbPaginaActual.Text = PaginaActual.ToString();
                dgInventario.ItemsSource = null;
                dgInventario.ItemsSource = inventario;

            }
        }

        private void SalirButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
