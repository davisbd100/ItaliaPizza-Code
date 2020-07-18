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
    public class ProductoDAOTests
    {
        [TestMethod()]
        public void GetProductoPorIDTest()
        {
            ProductoDAO productoDAO = new ProductoDAO();
            Assert.AreEqual(productoDAO.GetProductoPorID(1).idProducto, 1);
        }

        [TestMethod()]
        public void BuscarProductoTest()
        {
            ProductoDAO productoDAO = new ProductoDAO();
            Assert.IsTrue(productoDAO.BuscarProducto("papa").Any());
        }
    }
}