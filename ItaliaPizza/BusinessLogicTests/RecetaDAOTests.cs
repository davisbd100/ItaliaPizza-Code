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
    public class RecetaDAOTests
    {
        [TestMethod()]
        public void AddRecetaTest()
        {

            Receta receta = new Receta();
            receta.Nombre = "Pizza de papa";
            receta.Procedimiento = "Moverle bien duro a la masa";
            receta.Rendimiento = 2;

            List<ListaIngredientesReceta> listaIngredientesRecetas = new List<ListaIngredientesReceta>();
            ListaIngredientesReceta ingrediente = new ListaIngredientesReceta();
            ingrediente.Cantidad = 20;
            ingrediente.Código = "22";
            ingrediente.IdIngrediente = 1;
            listaIngredientesRecetas.Add(ingrediente);

            receta.Ingredientes = listaIngredientesRecetas;

            RecetaDAO recetaDAO = new RecetaDAO();
            Assert.AreEqual(recetaDAO.AddReceta(receta, 2), ResultadoOperacionEnum.ResultadoOperacion.Exito);

        }

        [TestMethod()]
        public void GetRecetasTest()
        {
            RecetaDAO recetaDAO = new RecetaDAO();
            Assert.IsTrue(recetaDAO.GetRecetas(1).Any());
        }

        [TestMethod()]
        public void ObtenerRecetaPorIdTest()
        {
            RecetaDAO recetaDAO = new RecetaDAO();
            Assert.IsTrue(recetaDAO.ObtenerRecetaPorId(1) != null);
        }

        [TestMethod()]
        public void ElimiarRecetaConProductosTest()
        {
            Receta receta = new Receta();
            receta.Nombre = "Pizza de papa";
            receta.Procedimiento = "Moverle bien duro a la masa";
            receta.Rendimiento = 2;
            RecetaDAO recetaDAO = new RecetaDAO();
            Assert.AreEqual(recetaDAO.ElimiarReceta(receta), ResultadoOperacionEnum.ResultadoOperacion.Exito);
        }

        [TestMethod()]
        public void EditarRecetaTest()
        {

            Receta receta = new Receta();
            receta.Nombre = "Pizza de papa";
            receta.Procedimiento = "Moverle bien duro a la masa";
            receta.Rendimiento = 2;

            List<ListaIngredientesReceta> listaIngredientesRecetas = new List<ListaIngredientesReceta>();
            ListaIngredientesReceta ingrediente = new ListaIngredientesReceta();
            ingrediente.Cantidad = 20;
            ingrediente.Código = "22";
            ingrediente.IdIngrediente = 1;
            listaIngredientesRecetas.Add(ingrediente);
            RecetaDAO recetaDAO = new RecetaDAO();
            Assert.AreEqual(recetaDAO.EditarReceta(receta), ResultadoOperacionEnum.ResultadoOperacion.Exito);

        }
    }
}