using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic;
using DatabaseConnection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static BusinessLogic.ResultadoOperacionEnum;

namespace BusinessLogicTest
{
    [TestClass]
    public class ClienteDAOTest
    {
        [TestMethod]
        public void DbConnectionTest()
        {
            DbConnection dbConnection = new DbConnection();
        }

        [TestMethod()]
        public void AgregarClienteExitoTest()
        {
            Cliente cliente = new Cliente();
            cliente.idPersona = "GABRIELASAGtOEndGl";
            cliente.Nombre = "Gabriela";
            cliente.Apellido = "Sandoval Cruz";
            cliente.Telefono = "2288574956";
            cliente.Email = "gabriela.uv@outlook.com";
            cliente.Ciudad = "Xalapa";
            cliente.Calle = "Gustavo diaz ordaz";
            cliente.Numero = "426";
            cliente.Colonia = "Predio de la virgen";
            cliente.CodigoPostal = "91150";
            cliente.idCliente = "GABRIELASAGtOEndGl";

            ClienteDAO clienteDAO = new ClienteDAO();
            Assert.AreEqual(ResultadoOperacion.Exito, clienteDAO.AgregarCliente(cliente));
        }

        [TestMethod()]
        public void AgregarClienteFalloSQLTest()
        {
            string idPersona = "ClienteGabi";
            string Nombre = "Gabriela";
            string Apellido = "Sandoval Cruz";
            string Telefono = "2288574956";
            string Email = "gabriela.uv@outlook.com";
            string Ciudad = "Xalapa";
            string Calle = "Gustavo diaz ordaz";
            string Numero = "426";
            string Colonia = "Predio de la virgen";
            string CodigoPostal = "91150";
            string idCliente = "GerenteGabi";

            Cliente cliente = new Cliente(idPersona, Nombre, Apellido, Telefono, Email, Ciudad, Calle, Numero, CodigoPostal, Colonia, idCliente);
            ClienteDAO clienteDAO = new ClienteDAO();
            Assert.AreEqual(ResultadoOperacion.Exito, clienteDAO.EditarCliente(cliente));
        }

        [TestMethod()]
        public void ObtenerListaClientesTest()
        {
            ClienteDAO clienteDAO = new ClienteDAO();
            List<Cliente> clientes = clienteDAO.GetCliente(1);
            Assert.AreEqual(true, clientes.Any());
        }

        [TestMethod()]
        public void EditarClienteExitoTest()
        {
            Cliente cliente = new Cliente();
            cliente.idPersona = "GABRIELASAGtOEndGl";
            cliente.Nombre = "Gabriela";
            cliente.Apellido = "Sandoval Cruz";
            cliente.Telefono = "2288574956";
            cliente.Email = "gabriela.uv@outlook.com";
            cliente.Ciudad = "Xalapa";
            cliente.Calle = "Gustavo diaz ordaz";
            cliente.Numero = "426";
            cliente.Colonia = "Predio de la virgen";
            cliente.CodigoPostal = "91150";
            cliente.idCliente = "GABRIELASAGtOEndGl";

            ClienteDAO clienteDAO = new ClienteDAO();
            Assert.AreEqual(ResultadoOperacion.Exito, clienteDAO.EditarCliente(cliente));
        }

        [TestMethod()]
        public void EditarClienteFalloSQLTest()
        {
            Cliente cliente = new Cliente();
            cliente.idPersona = "Gabriela";
            cliente.Nombre = "Gabriela";
            cliente.Apellido = "Sandoval Cruz";
            cliente.Telefono = "2288574956";
            cliente.Email = "gabriela.uv@outlook.com";
            cliente.Ciudad = "Xalapa";
            cliente.Calle = "Gustavo diaz ordaz";
            cliente.Numero = "426";
            cliente.Colonia = "Predio de la virgen";
            cliente.CodigoPostal = "91150";
            cliente.idCliente = "Gabriela";

            ClienteDAO clienteDAO = new ClienteDAO();
            Assert.AreEqual(ResultadoOperacion.FalloSQL, clienteDAO.EditarCliente(cliente));
        }

        [TestMethod()]
        public void DarDeBajaClienteExitoTest()
        {
            string idPersonaPrueba = "GABRIELASAGtOEndGl";
            ClienteDAO clienteDAO = new ClienteDAO();
            Assert.AreEqual(ResultadoOperacion.Exito, clienteDAO.EliminarCliente(idPersonaPrueba));
        }

        [TestMethod()]
        public void BuscarClienteExitoTest()
        {
            string busqueda = "Gabriela";
            ClienteDAO clienteDAO = new ClienteDAO();
            List<Cliente> clientes = clienteDAO.BuscarCliente(busqueda + "%");
            Assert.AreEqual(true, clientes.Any());
        }

        [TestMethod()]
        public void BuscarClienteDireccionExitoTest()
        {
            string busqueda = "Gustavo diaz Ordaz";
            ClienteDAO clienteDAO = new ClienteDAO();
            List<Cliente> clientes = clienteDAO.BuscarCliente(busqueda + "%");
            Assert.AreEqual(false, clientes.Any());
        }

        [TestMethod()]
        public void BuscarClienteTelefonoExitoTest()
        {
            string busqueda = "2288574956";
            ClienteDAO clienteDAO = new ClienteDAO();
            List<Cliente> clientes = clienteDAO.BuscarCliente(busqueda + "%");
            Assert.AreEqual(false, clientes.Any());
        }

        [TestMethod()]
        public void BuscarClienteFalloSQLTest()
        {
            string busqueda = "Hola";
            ClienteDAO clienteDAO = new ClienteDAO();
            List<Cliente> clientes = clienteDAO.BuscarCliente(busqueda + "%");
            Assert.AreEqual(false, clientes.Any());
        }
    }
}
