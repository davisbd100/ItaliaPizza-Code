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
        ResultadoOperacion AddReceta(Receta receta);
        List<Receta> GetRecetas(int rango);
        Receta ObtenerRecetaPorId(int idReceta);



    }
}
