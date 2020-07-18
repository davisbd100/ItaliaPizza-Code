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
    public class ProductoIngredienteDAOTests
    {
        [TestMethod()]
        public void AddProductoIngredienteTest()
        {

            ProductoIngrediente productoIngrediente = new ProductoIngrediente();
            productoIngrediente.Código = "xxx";
            productoIngrediente.Descripción = "Papa";
            productoIngrediente.Nombre = "Papa";
            productoIngrediente.Restricción = "ninguna";
            productoIngrediente.tipoIngrediente = TipoIngredienteEnum.Verdura;
            Inventario inventario = new Inventario();
            inventario.Caducidad = DateTime.Now.ToString();
            inventario.FechaIngreso = DateTime.Now;
            inventario.ExistenciaTotal = 1;
            ProductoIngredienteDAO productoIngredienteDAO = new ProductoIngredienteDAO();
            Assert.AreEqual(productoIngredienteDAO.AddProductoIngrediente(productoIngrediente, inventario),
                ResultadoOperacionEnum.ResultadoOperacion.Exito);

        }

        [TestMethod()]
        public void GetProductosIngredienteTest()
        {
            ProductoIngredienteDAO productoIngrediente = new ProductoIngredienteDAO();
            Assert.IsTrue(productoIngrediente.GetProductosIngrediente(1).Any());

        }

        [TestMethod()]
        public void EditarProductoTest()
        {
            ProductoIngrediente productoIngrediente = new ProductoIngrediente();
            productoIngrediente.Código = "xxx";
            productoIngrediente.Descripción = "Papa";
            productoIngrediente.Nombre = "Papa";
            productoIngrediente.Restricción = "ninguna";
            ProductoIngredienteDAO productoIngredienteDAO = new ProductoIngredienteDAO();
            Assert.AreEqual(productoIngredienteDAO.EditarProducto(productoIngrediente), ResultadoOperacionEnum.ResultadoOperacion.Exito);
        }

        [TestMethod()]
        public void EliminarProductoTest()
        {
            ProductoIngredienteDAO productoIngrediente = new ProductoIngredienteDAO();
            Assert.AreEqual(productoIngrediente.EliminarProducto(2), ResultadoOperacionEnum.ResultadoOperacion.Exito);

        }
    }
}