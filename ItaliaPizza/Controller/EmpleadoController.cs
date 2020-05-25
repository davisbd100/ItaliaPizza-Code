using BusinessLogic;
using System;
using static BusinessLogic.ResultadoOperacionEnum;

namespace Controller
{
    public class EmpleadoController
    {
        public ResultadoOperacion AgregarEmpleado(String nombre, String apellido,
            String telefono, String email, String ciudad, String calle, String numero,
            String colonia, String codigoPostal, TipoEmpleado tipoEmpleado, String usuario, 
            String contrasena)
        {
            ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;

            if (GetEmpleadoByUsername(usuario).NombreUsuario == null)
            {
                Empleado empleado = new Empleado();
                empleado.Nombre = nombre;
                empleado.Apellido = apellido;
                empleado.Telefono = telefono;
                empleado.Email = email;
                empleado.Ciudad = ciudad;
                empleado.Calle = calle;
                empleado.Numero = numero;
                empleado.Colonia = colonia;
                empleado.CodigoPostal = codigoPostal;
                empleado.tipoEmpleado = tipoEmpleado;
                empleado.NombreUsuario = usuario;
                empleado.Contraseña = contrasena;
                EmpleadoDAO empleadoDAO = new EmpleadoDAO();
                resultado = (ResultadoOperacion)empleadoDAO.AgregarEmpleado(empleado);
            }
            else
            {
                resultado = ResultadoOperacion.ObjetoExistente;
            }

            return resultado;
        }

        public Empleado GetEmpleadoByUsername(String username)
        {
            EmpleadoDAO empleadoDAO = new EmpleadoDAO();
            return empleadoDAO.GetEmpleadoByUsername(username);
        }
    }
}
