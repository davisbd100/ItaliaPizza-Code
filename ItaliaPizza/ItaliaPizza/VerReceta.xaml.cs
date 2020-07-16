﻿using BusinessLogic;
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
    /// Interaction logic for VerReceta.xaml
    /// </summary>
    public partial class VerReceta : Window
    {
        public VerReceta(Receta recetaEnviada)
        {
            InitializeComponent();
            receta = recetaEnviada;
            llenarCampos();
        }

        Receta receta = new Receta();


        private void llenarCampos()
        {
            tb_Nombre.Text = receta.Nombre;
            tb_Rendimiento.Text = receta.Rendimiento.ToString();
            tb_Procedimiento.Text = receta.Procedimiento.ToString();
        }


        private void btn_Aceptar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
