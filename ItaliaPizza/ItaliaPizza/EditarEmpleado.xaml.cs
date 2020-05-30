using BusinessLogic;
using Controller;
using System.Windows;

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
            CargarCampos();
        }

        Empleado empleadoEditar = new Empleado();

        private void CargarCampos()
        {
            EmpleadoController empleadoController = new EmpleadoController();
            Empleado datosEmpleado = empleadoController.GetEmpleadoId(empleadoEditar.idEmpleado);
            comboBoxTipoEmpleado.Text = datosEmpleado.TipoEmpleado;
            textBoxNombre.Text = datosEmpleado.Nombre;
            textBoxApellido.Text = datosEmpleado.Apellido;
            textBoxTelefono.Text = datosEmpleado.Telefono;
            textBoxCorreo.Text = datosEmpleado.Email;
            textBoxCiudad.Text = datosEmpleado.Ciudad;
            textBoxCalle.Text = datosEmpleado.Calle;
            textBoxNúmero.Text = datosEmpleado.Numero;
            textBoxColonia.Text = datosEmpleado.Colonia;
            textBoxCodigoPostal.Text = datosEmpleado.CodigoPostal;
            textBoxIdEmpleado.Text = datosEmpleado.idEmpleado;
            textBoxUsuario.Text = datosEmpleado.NombreUsuario;
            textBoxContraseña.Text = datosEmpleado.Contraseña;
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
