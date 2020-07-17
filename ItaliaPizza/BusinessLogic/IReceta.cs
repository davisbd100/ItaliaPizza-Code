using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BusinessLogic.ResultadoOperacionEnum;

namespace BusinessLogic
{
    interface IReceta
    {
        ResultadoOperacion AddReceta(Receta receta, int productoVentaID);
        List<Receta> GetRecetas(int rango);
        Receta ObtenerRecetaPorId(int idReceta);
        ResultadoOperacion ElimiarReceta(Receta receta);
        ResultadoOperacion ElimiarRecetaConProductos(Receta receta);
        ResultadoOperacion EditarReceta(Receta receta);
        List<ListaIngredientesReceta> obtenerProductosReceta(int id);


    }
}
