using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using Controller;

namespace ItaliaPizza
{
    /// <summary>
    /// Lógica de interacción para EditarEmpleado.xaml
    /// </summary>
    public partial class RespaldarInformacion : Window
    {
        public RespaldarInformacion()
        {
            InitializeComponent();
        }

        private void btExaminar_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.FileName = "folderselection";
            fileDialog.CheckFileExists = false;
            fileDialog.CheckPathExists = true;
            Nullable<bool> dialogOK = fileDialog.ShowDialog();

            if (dialogOK == true)
            {
                string folder = System.IO.Path.GetFullPath(fileDialog.FileName);
                folder = folder.Replace("folderselection", "");
                tbRuta.Text = folder;
                btGuardar.IsEnabled = true;
            }
        }

        private void btGuardar_Click(object sender, RoutedEventArgs e)
        {
            BaseDeDatosController controller = new BaseDeDatosController();
            if (!Directory.Exists(tbRuta.Text))
            {
                MessageBox.Show("La ruta ingresada no existe");
            }
            else
            {
                switch (controller.IniciarRespaldo(tbRuta.Text))
                {
                    case BusinessLogic.ResultadoOperacionEnum.ResultadoOperacion.FalloSQL:
                        MessageBox.Show("No se pudo obtener el respaldo");
                        break;
                    case BusinessLogic.ResultadoOperacionEnum.ResultadoOperacion.Exito:
                        MessageBox.Show("Respaldo realizado con exito");
                        this.Close();
                        break;
                    case BusinessLogic.ResultadoOperacionEnum.ResultadoOperacion.FallaDesconocida:
                        MessageBox.Show("Error no identificado");
                        break;
                }
            }
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
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
