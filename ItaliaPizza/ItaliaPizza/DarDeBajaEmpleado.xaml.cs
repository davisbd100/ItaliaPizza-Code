using BusinessLogic;
using Controller;
using DataAccess;
using System;
using System.Windows;
using static BusinessLogic.ResultadoOperacionEnum;

namespace ItaliaPizza
{
    /// <summary>
    /// Lógica de interacción para DarDeBajaEmpleado.xaml
    /// </summary>
    public partial class DarDeBajaEmpleado : Window
    {
        public DarDeBajaEmpleado(BusinessLogic.Empleado empleadoEliminar)
        {
            InitializeComponent();
            empleadoE = empleadoEliminar;
        }

        public enum OperationResult
        {
            Success,
            NullOrganization,
            InvalidOrganization,
            UnknowFail,
            SQLFail,
            ExistingRecord
        }

        BusinessLogic.Empleado empleadoE = new BusinessLogic.Empleado();

        private void CancelarButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Está seguro que desea cancelar?", "Cancelar", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Close();
                    break;
                case MessageBoxResult.No:
                    RegistrarEmpleado registrarEmpleado = new RegistrarEmpleado();
                    registrarEmpleado.Close();
                    break;
            }
        }

        private void AceptarButton_Click(object sender, RoutedEventArgs e)
        {
            EliminarEmpleado();
        }

        private void EliminarEmpleado()
        {
            EmpleadoController empleadoController = new EmpleadoController();
            if(empleadoController.EliminarEmpleado(empleadoE) == ResultadoOperacion.Exito)
            { 
                MessageBox.Show("Producto eliminado con éxito");
            }
            else
            {
                MessageBox.Show("No se pudo eliminar el producto");
            }
        }

        private void ComprobarResultado(OperationResult result)
        {
            if (result == OperationResult.Success)
            {
                MessageBox.Show("Operacion realizada con exito");
                this.Close();
            }
            else if (result == OperationResult.UnknowFail)
            {
                MessageBox.Show("Error desconocido");
            }
            else if (result == OperationResult.SQLFail)
            {
                MessageBox.Show("Error de la base de datos, intente mas tarde");
            }
        }
    }
}
