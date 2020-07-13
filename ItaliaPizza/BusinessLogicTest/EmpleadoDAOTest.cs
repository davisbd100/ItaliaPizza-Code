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
    public class EmpleadoDAOTest
    {
        [TestMethod]
        public void DbConnectionTest()
        {
            DbConnection dbConnection = new DbConnection();
        }

        [TestMethod()]
        public void AgregarEmpleadoExitoTest()
        {
            Empleado empleado = new Empleado();
            empleado.idPersona = "GERENTEGiGJbTFp@o";
            empleado.Nombre = "Gabriela";
            empleado.Apellido = "Sandoval Cruz";
            empleado.Telefono = "2288574956";
            empleado.Email = "gabriela.uv@outlook.com";
            empleado.Ciudad = "Xalapa";
            empleado.Calle = "Gustavo diaz ordaz";
            empleado.Numero = "426";
            empleado.Colonia = "Predio de la virgen";
            empleado.CodigoPostal = "91150";
            empleado.idEmpleado = "GERENTEGiGJbTFp@o";
            empleado.NombreUsuario = "GERENTEGabr";
            empleado.Contraseña = "GabrielaSa";
            empleado.FechaUltimoAcceso = DateTime.Now;
            empleado.TipoEmpleado = "Gerente";
            EmpleadoDAO empleadoDAO = new EmpleadoDAO();

            Assert.AreEqual(ResultadoOperacion.Exito, empleadoDAO.AgregarEmpleado(empleado));
        }

        [TestMethod()]
        public void AgregarEmpleadoFalloSQLTest()
        {
            string idPersona = "GerenteGabi";
            string Nombre = "Gabriela";
            string Apellido = "Sandoval Cruz";
            string Telefono = "2288574956";
            string Email = "gabriela.uv@outlook.com";
            string Ciudad = "Xalapa";
            string Calle = "Gustavo diaz ordaz";
            string Numero = "426";
            string Colonia = "Predio de la virgen";
            string CodigoPostal = "91150";
            string idEmpleado = "GerenteGabi";
            string NombreUsuario = "GerenteGabriela";
            string Contrasena = "GabrielaSandoval";
            DateTime FechaUltimoAcceso = DateTime.Now;
            string TipoEmpleado = "Gerente";

            Empleado empleado = new Empleado(idPersona, Nombre, Apellido, Telefono, Email, Ciudad, Calle, Numero, CodigoPostal, Colonia, idEmpleado, NombreUsuario, Contrasena, FechaUltimoAcceso, TipoEmpleado);
            EmpleadoDAO empleadoDAO = new EmpleadoDAO();

            Assert.AreEqual(ResultadoOperacion.FalloSQL, empleadoDAO.AgregarEmpleado(empleado));
        }

        [TestMethod()]
        public void EditarEmpleadoExitoTest()
        {
            Empleado empleado = new Empleado();
            empleado.idPersona = "GERENTEGiGJbTFp@o";
            empleado.Nombre = "Gabriela";
            empleado.Apellido = "Sandoval Cruz";
            empleado.Telefono = "2288574956";
            empleado.Email = "gabriela.uv@outlook.com";
            empleado.Ciudad = "Xalapa";
            empleado.Calle = "Gustavo diaz ordaz";
            empleado.Numero = "426";
            empleado.Colonia = "Predio de la virgen";
            empleado.CodigoPostal = "91150";
            empleado.idEmpleado = "GERENTEGiGJbTFp@o";
            EmpleadoDAO empleadoDAO = new EmpleadoDAO();

            Assert.AreEqual(ResultadoOperacion.Exito, empleadoDAO.EditarEmpleado(empleado));
        }

        [TestMethod()]
        public void EditarEmpleadoFalloSQLTest()
        {
            Empleado empleado = new Empleado();
            empleado.Nombre = "Gabriela";
            empleado.Apellido = "Sandoval Cruz";
            empleado.Telefono = "2288574956";
            empleado.Email = "gabriela.uv@outlook.com";
            empleado.Ciudad = "Xalapa";
            empleado.Calle = "Gustavo diaz ordaz";
            empleado.Numero = "426";
            empleado.Colonia = "Predio de la virgen";
            empleado.CodigoPostal = "91150";
            empleado.idEmpleado = "GERENTEGiGJbTFp@o";
            EmpleadoDAO empleadoDAO = new EmpleadoDAO();

            Assert.AreEqual(ResultadoOperacion.FalloSQL, empleadoDAO.EditarEmpleado(empleado));
        }


        [TestMethod()]
        public void ObtenerListaEmpleadosTest()
        {
            EmpleadoDAO empleadoDAO = new EmpleadoDAO();
            List<Empleado> listEmpleados = empleadoDAO.GetEmpleados(1);
            Assert.AreEqual(false, listEmpleados.Any());
        }

        [TestMethod()]
        public void DarDeBajaEmpleadosExitoTest()
        {
            string idPersonaPrueba = "GERENTEGiGJbTFp@o";
            EmpleadoDAO empleadoDAO = new EmpleadoDAO();
            Assert.AreEqual(ResultadoOperacion.Exito, empleadoDAO.EliminarEmpleado(idPersonaPrueba));
        }

        [TestMethod()]
        public void BuscarEmpleadoExitoTest()
        {
            string busqueda = "Gabriela";
            EmpleadoDAO empleadoDAO = new EmpleadoDAO();
            List<Empleado> empleados = empleadoDAO.BuscarEmpleado(busqueda + "%");
            Assert.AreEqual(true, empleados.Any());
        }

        [TestMethod()]
        public void BuscarEmpleadoDireccionExitoTest()
        {
            string busqueda = "Gustavo diaz Ordaz";
            EmpleadoDAO empleadoDAO = new EmpleadoDAO();
            List<Empleado> empleados = empleadoDAO.BuscarEmpleado(busqueda + "%");
            Assert.AreEqual(false, empleados.Any());
        }

        [TestMethod()]
        public void BuscarEmpleadoTelefonoExitoTest()
        {
            string busqueda = "2288574956";
            EmpleadoDAO empleadoDAO = new EmpleadoDAO();
            List<Empleado> empleados = empleadoDAO.BuscarEmpleado(busqueda + "%");
            Assert.AreEqual(false, empleados.Any());
        }

        [TestMethod()]
        public void BuscarEmpleadoFalloSQLTest()
        {
            string busqueda = "Hola";
            EmpleadoDAO empleadoDAO = new EmpleadoDAO();
            List<Empleado> empleados = empleadoDAO.BuscarEmpleado(busqueda + "%");
            Assert.AreEqual(false, empleados.Any());
        }
    }
}
