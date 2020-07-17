using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Tests
{
    [TestClass()]
    public class VentaDAOTests
    {
        [TestMethod()]
        public void GuardarDiaVentaTest()
        {
            VentaDAO ventaDAO = new VentaDAO();
            DataAccess.DiaVenta dia = new DataAccess.DiaVenta()
            {
                Fecha = DateTime.Now,
                Ingresos = 0
            };
            Assert.AreEqual(ventaDAO.GuardarDiaVenta(dia), ResultadoOperacionEnum.ResultadoOperacion.Exito);
        }
    }
}