using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutenticacionInicioSesion;

namespace AutenticacionInicioSesionTest
{
    [TestClass]
    public class AutenticacionInicioSesionTest
    {
        [TestMethod()]
        public void CredentialsAuthenticationTest()
        {
            Autenticacion autenticacion = new Autenticacion();
            Assert.AreEqual(autenticacion.AutenticacionCredenciales("GerenteGaby", "Gabriela"), Autenticacion.validationResult.Success);
        }
    }
}
