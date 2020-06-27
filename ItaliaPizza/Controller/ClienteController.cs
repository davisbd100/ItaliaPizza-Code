using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BusinessLogic.ResultadoOperacionEnum;

namespace Controller
{
	public class ClienteController
	{
		public ResultadoOperacion AgregarCliente(String idPersona, String nombre, String apellido,
			String telefono, String email, String ciudad, String calle, String numero,
			String colonia, String codigoPostal, String idCliente)
		{
			ResultadoOperacion resultado = ResultadoOperacion.FallaDesconocida;

			Cliente cliente = new Cliente();
			cliente.idPersona = idPersona;
			cliente.Nombre = nombre;
			cliente.Apellido = apellido;
			cliente.Telefono = telefono;
			cliente.Email = email;
			cliente.Ciudad = ciudad;
			cliente.Calle = calle;
			cliente.Numero = numero;
			cliente.Colonia = colonia;
			cliente.CodigoPostal = codigoPostal;
			cliente.idCliente = cliente.idPersona;

			ClienteDAO clienteDAO = new ClienteDAO();
			resultado = (ResultadoOperacion)clienteDAO.AgregarCliente(cliente);

			return resultado;
		}

		public Cliente GetClienteByIdCliente(string idCliente)
		{
			ClienteDAO clienteDAO = new ClienteDAO();
			return clienteDAO.GetClineteByIdCliente(idCliente);
		}

		public ResultadoOperacion EliminarCliente(Cliente cliente)
        {
			ClienteDAO clienteDAO = new ClienteDAO();
			ResultadoOperacion resultado = clienteDAO.EliminarCliente(cliente.idCliente);
			return resultado;
        }
	}
}
