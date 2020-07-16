using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.ResultadoOperacionEnum;

namespace Controller
{
    public class RecetaController
    {
        public ResultadoOperacion CrearReceta(string nombre, string procedimiento, string rendimiento,
            List<ListaIngredientesReceta> productos, int productoVenta)
        {
            ResultadoOperacion resultadoOperacion = new ResultadoOperacion();

            List <ListaIngredientesReceta> listaIngredientesReceta = new List<ListaIngredientesReceta>();

            Receta receta = new Receta();
            receta.Nombre = nombre;
            receta.Procedimiento = procedimiento;
            receta.Rendimiento = float.Parse(rendimiento);
            receta.Ingredientes = productos;

            RecetaDAO recetaDAO = new RecetaDAO();
            resultadoOperacion = recetaDAO.AddReceta(receta, productoVenta);

            return resultadoOperacion;
        }


        public List<Receta> ObtenerTodasRecetas(int pagina)
        {
            pagina--;
            RecetaDAO recetaDAO = new RecetaDAO();
            return recetaDAO.GetRecetas(pagina);
        }
    }
}
