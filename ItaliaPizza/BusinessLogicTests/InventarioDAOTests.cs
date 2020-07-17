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
    public class InventarioDAOTests
    {
        [TestMethod()]
        public void AddInventarioTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ModificarInventarioTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerInventariosTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerTodosLosInventariosTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ActualizarInventarioTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerTodosLosInventariosConIngresoTest()
        {
            InventarioDAO inventarioDAO = new InventarioDAO();
            var listaInventarios = inventarioDAO.ObtenerTodosLosInventarios();
            Assert.IsTrue(listaInventarios.Any());
        }

        [TestMethod()]
        public void ObtenerProductoInventarioTest()
        {
            InventarioDAO inventarioDAO = new InventarioDAO();
            var listaInventarios = inventarioDAO.ObtenerProductoInventario(1);
            Assert.IsTrue(listaInventarios.Any());
        }

        [TestMethod()]
        public void ObtenerPaginasDeTablaInventarioTest()
        {
            InventarioDAO inventarioDAO = new InventarioDAO();
            var paginas = inventarioDAO.ObtenerPaginasDeTablaInventario(20);
            bool resultado = false;
            if (paginas > -1)
            {
                resultado = true;
            }
            Assert.IsTrue(resultado);
        }

        [TestMethod()]
        public void ObtenerInventarioPorRangoTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObtenerPaginasDeTablaDiaVentaTest()
        {
            InventarioDAO inventarioDAO = new InventarioDAO();
            var paginas = inventarioDAO.ObtenerPaginasDeTablaDiaVenta(20);
            bool resultado = false;
            if (paginas > -1)
            {
                resultado = true;
            }
            Assert.IsTrue(resultado);
        }

        [TestMethod()]
        public void ObtenerDiaVentaPorRangoTest()
        {
            InventarioDAO diaVentaDAO = new InventarioDAO();
            var listaDiaVenta = diaVentaDAO.ObtenerDiaVentaPorRango(20, 1);
            bool resultado = true;
            if (listaDiaVenta.Count > 19 || listaDiaVenta.Count < 0)
            {
                resultado = false;
            }
            Assert.IsTrue(resultado);
        }

        [TestMethod()]
        public void ObtenerProductoVentaDiaTest()
        {
            InventarioDAO inventarioDAO = new InventarioDAO();
            var listaDiaVenta = inventarioDAO.ObtenerProductoVentaDia(1);
            Assert.IsTrue(listaDiaVenta.Any());
        }

        [TestMethod()]
        public void CerrarDiaTest()
        {
            Assert.Fail();
        }
    }
}