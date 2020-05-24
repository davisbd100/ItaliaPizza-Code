﻿using System;
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
using BusinessLogic;
using Controller;

namespace PrototiposItaliaPizza
{
    /// <summary>
    /// Lógica de interacción para DarDeBajaCliente.xaml
    /// </summary>
    public partial class CancelarPedido : Window
    {

        Pedido localPedido { get; set; }
        public CancelarPedido(Pedido pedido)
        {
            localPedido = pedido;
            InitializeComponent();
        }

        private void btAceptar_Click(object sender, RoutedEventArgs e)
        {
            PedidoController controller = new PedidoController();
            ResultadoOperacionEnum.ResultadoOperacion resultado = controller.CancelarPedido(localPedido);
            if (resultado == ResultadoOperacionEnum.ResultadoOperacion.Exito)
            {
                MessageBox.Show("Pedido Cancelado");
            }else if(resultado == ResultadoOperacionEnum.ResultadoOperacion.FalloSQL)
            {
                MessageBox.Show("Error con la base de datos, reintentar mas tarde");
            }
            this.Close();
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btAceptar_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
