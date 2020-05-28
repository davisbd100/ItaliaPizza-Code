using BusinessLogic;
using System;
using System.Collections.Generic;
using static BusinessLogic.ResultadoOperacionEnum;

namespace Controller
{
	public class EmpleadoController
	{
		public ResultadoOperacion AgregarEmpleado(String idPersona, String nombre, String apellido,
			String telefono, String email, String ciudad, String calle, String numero,
			String colonia, String codigoPostal, String idEmpleado, String usuario, String contrasena, 
			String tipoEmpleado)
		{
			ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;

			if (GetEmpleadoByUsername(usuario).NombreUsuario == null)
			{
				Empleado empleado = new Empleado();
				empleado.idPersona = idPersona;
				empleado.Nombre = nombre;
				empleado.Apellido = apellido;
				empleado.Telefono = telefono;
				empleado.Email = email;
				empleado.Ciudad = ciudad;
				empleado.Calle = calle;
				empleado.Numero = numero;
				empleado.Colonia = colonia;
				empleado.CodigoPostal = codigoPostal;
				empleado.idEmpleado = empleado.idPersona;
				empleado.NombreUsuario = usuario;
				empleado.Contraseña = contrasena;
				empleado.FechaUltimoAcceso = DateTime.Now;
				empleado.TipoEmpleado = tipoEmpleado;
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

		public List<Empleado> GetEmpleado(int rango)
		{
			const int Num_RESULTADOS = 19;
			rango -= 1;
			rango *= Num_RESULTADOS;

			EmpleadoDAO empleadoDAO = new EmpleadoDAO();
			List<Empleado> empleados = empleadoDAO.GetEmpleados(rango);
			return empleados;
		}

		public List<Empleado> BuscarEmpleado(string busqueda)
		{
			EmpleadoDAO empleadoDAO = new EmpleadoDAO();
			List<Empleado> empleados = empleadoDAO.BuscarEmpleado(busqueda + "%");
			return empleados;
		}

		public List<Empleado> BuscarEmpleadoDireccion(string busqueda)
		{
			EmpleadoDAO empleadoDAO = new EmpleadoDAO();
			List<Empleado> empleados = empleadoDAO.BuscarEmpleadoDireccion(busqueda + "%");
			return empleados;
		}

		public List<Empleado> BuscarEmpleadoTelefono(string busqueda)
		{
			EmpleadoDAO empleadoDAO = new EmpleadoDAO();
			List<Empleado> empleados = empleadoDAO.BuscarEmpleadoTelefono(busqueda + "%");
			return empleados;
		}
	}
}
