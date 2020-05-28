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
    /// Lógica de interacción para EditarEmpleado.xaml
    /// </summary>
    public partial class EditarEmpleado : Window
    {
        public EditarEmpleado(Empleado empleado)
        {
            InitializeComponent();
            empleadoEditar = empleado;
            CargarCampos(empleadoEditar.idEmpleado);
        }

        Empleado empleadoEditar = new Empleado();

        private void CargarCampos(string id)
        {
            EmpleadoController empleadoController = new EmpleadoController();
            Empleado empleado = empleadoController.GetEmpleadoId(id);
            comboBoxTipoEmpleado.Text = empleado.TipoEmpleado;
            textBoxNombre.Text = empleado.Nombre;
            textBoxApellido.Text = empleado.Apellido;
            textBoxTelefono.Text = empleado.Telefono;
            textBoxCorreo.Text = empleado.Email;
            textBoxCiudad.Text = empleado.Ciudad;
            textBoxCalle.Text = empleado.Calle;
            textBoxNúmero.Text = empleado.Numero;
            textBoxColonia.Text = empleado.Colonia;
            textBoxCodigoPostal.Text = empleado.CodigoPostal;
            textBoxIdEmpleado.Text = empleado.idEmpleado;
            textBoxUsuario.Text = empleado.NombreUsuario;
            textBoxContraseña.Text = empleado.Contraseña;
        }

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


    }
}
